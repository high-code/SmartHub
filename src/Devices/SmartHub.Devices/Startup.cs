using System;
using System.Data.Common;
using System.Security.Claims;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using SmartHub.BusinessLogic.Contracts;
using SmartHub.BusinessLogic.Services;
using SmartHub.Devices.Application;
using SmartHub.Edge.Infrastructure;
using SmartHub.Infrastructure;
using SmartHub.Infrastructure.Concrete;
using SmartHub.Infrastructure.Contracts;
using SmartHub.IntegrationEventLog;
using SmartHub.IntegrationEventLog.Services;
using SmartHub.Messaging;
using SmartHub.Messaging.Abstractions;
using SmartHub.Messaging.RabbitMQ;

namespace SmartHub.Api
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
      services.AddMvc();

      services.AddEntityFrameworkNpgsql().AddDbContext<SmartHubContext>(opt =>
        opt.UseNpgsql(Configuration["ConnectionString"]));

      services.AddEntityFrameworkNpgsql().AddDbContext<IntegrationEventLogContext>(o =>
      {
        o.UseNpgsql(Configuration["ConnectionString"],
          sqlOptions =>
          {
            sqlOptions.MigrationsAssembly("SmartHub.Infrastructure");
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
          });
      }, ServiceLifetime.Scoped);
      services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
      }));

      var authorityUrl = Configuration["AuthorityUrl"];
      services.AddAuthentication(options =>
          {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
        .AddJwtBearer(options =>
        {
          options.Authority = authorityUrl;
          options.MetadataAddress = authorityUrl + "/.well-known/openid-configuration";
          options.RequireHttpsMetadata = false;
          options.Audience = "smarthub";
          options.IncludeErrorDetails = true;
          options.TokenValidationParameters = new TokenValidationParameters()
          {
            NameClaimType = ClaimTypes.NameIdentifier
          };

        });

      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IDeviceRepository, DeviceRepository>();
      services.AddScoped<IDeviceService, DeviceService>();
      services.AddCustomIntegrations(Configuration);
      services.AddEventBus(Configuration);
      services.AddHttpContextAccessor();
      var container = new ContainerBuilder();

      container.RegisterModule(new MediatorModule());
      container.Populate(services);


      return new AutofacServiceProvider(container.Build());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseCors("MyPolicy");
      app.UseAuthentication();
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

  public static class StartupExtensions
  {
    public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
               sp => (DbConnection c) => new IntegrationEventLogService(c));

      services.AddTransient<IDeviceIntegrationEventLogService, DeviceIntegrationEventLogService>();

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


