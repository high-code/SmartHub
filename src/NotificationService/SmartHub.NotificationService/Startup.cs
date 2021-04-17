using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using SmartHub.Messaging;
using SmartHub.Messaging.Abstractions;
using SmartHub.Messaging.RabbitMQ;
using SmartHub.NotificationService.Hubs;
using SmartHub.NotificationService.Infrastructure;
using SmartHub.NotificationService.IntegrationEvents.EventHandling;
using SmartHub.NotificationService.IntegrationEvents.Events;

namespace SmartHub.NotificationService
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });
      services.AddLogging(
        loggingBuilder => loggingBuilder.AddSeq(Configuration.GetSection("Seq")))
        .AddCustomIntegrations(Configuration)
        .AddEventBus(Configuration);
      services.AddSignalR().AddRedis(
        Configuration["RedisURI"], options => options.Configuration.ClientName = "NotificationService");
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      var container = new ContainerBuilder();

      container.RegisterModule(new ApplicationModule());
      container.Populate(services);

      return new AutofacServiceProvider(container.Build());
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");

        app.UseHsts();
      }

      if (env.IsDevelopment())
      {
        app.UseCors(builder =>
        {
          builder.WithOrigins("https://localhost:44332")
            .AllowAnyHeader()
            .WithMethods("GET", "POST")
            .AllowCredentials();
        });
      }

      app.UseSignalR(builder => builder.MapHub<TelemetryHub>("/telemetry"));
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseMvc();
      ConfigureEventBus(app);
    }


    private void ConfigureEventBus(IApplicationBuilder app)
    {
      var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

      eventBus.Subscribe<MeasurementReceivedIntegrationEvent, MeasurementReceivedIntegrationEventHandler>();
    }
  }

  public static class ServiceCollectionExtensions
  {

    public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
    {

      services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
      {
        var factory = new ConnectionFactory()
        {
          HostName = configuration["EventBusConnection"],
          DispatchConsumersAsync = true
        };

        if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
        {
          factory.UserName = configuration["EventBusUserName"];
        }

        if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
        {
          factory.Password = configuration["EventBusPassword"];
        }

        return new DefaultRabbitMQPersistentConnection(factory, null);
      });
      return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
      var subscriptionClientName = configuration["SubscriptionClientName"];

      services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
      {
        var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
        var lifetimeScope = sp.GetRequiredService<ILifetimeScope>();

        var eventBusSubscriptionManager = sp.GetRequiredService<IEventBusSubscriptionManager>();

        return new EventBusRabbitMQ(rabbitMqPersistentConnection, eventBusSubscriptionManager, lifetimeScope, null, 5, subscriptionClientName);
      });

      services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>();

      return services;
    }
  }
}
