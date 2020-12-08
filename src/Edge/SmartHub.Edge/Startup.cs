using System;
using System.Data.Common;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SmartHub.Edge.Application.IntegrationEvents;
using SmartHub.Edge.Infrastructure;
using SmartHub.IntegrationEventLog;
using SmartHub.IntegrationEventLog.Services;
using SmartHub.Messaging;
using SmartHub.Messaging.Abstractions;
using SmartHub.Messaging.RabbitMQ;

namespace SmartHub.Edge
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
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddCustomDbContext(Configuration)
              .AddCustomIntegrations(Configuration)
              .AddEventBus(Configuration);

      services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
      }));

      var container = new ContainerBuilder();

      container.RegisterModule(new ApplicationModule());
      container.RegisterModule(new MediatorModule());
      container.Populate(services);


      return new AutofacServiceProvider(container.Build());
    }

  

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseCors("MyPolicy");
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }

  public static class ServicesCollectionExtensions
  {

    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {

      services.AddEntityFrameworkNpgsql().AddDbContext<EdgeDbContext>(o =>
      {
        o.UseNpgsql(configuration.GetConnectionString("Default"),
          sqlOptions =>
          {
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
          });
      }, ServiceLifetime.Scoped);
      services.AddEntityFrameworkNpgsql().AddDbContext<IntegrationEventLogContext>(o =>
      {
        o.UseNpgsql(configuration.GetConnectionString("Default"),
          sqlOptions =>
          {
            sqlOptions.MigrationsAssembly("SmartHub.Edge");
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
          });
      }, ServiceLifetime.Scoped);
      return services;
    }

    public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
               sp => (DbConnection c) => new IntegrationEventLogService(c));

      services.AddTransient<IEdgeIntegrationEventLogService, EdgeIntegrationEventService>();

      services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
      {
        var factory = new ConnectionFactory()
        {
          HostName = configuration["EventBusConnection"],
          DispatchConsumersAsync = true
        };

        if(!string.IsNullOrEmpty(configuration["EventBusUserName"]))
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

        return new EventBusRabbitMQ(rabbitMqPersistentConnection, eventBusSubscriptionManager, lifetimeScope,null, 5, subscriptionClientName);
      });

      services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>();

      return services;
    }
  }
}
