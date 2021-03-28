using System.Collections.Generic;
using Autofac;
using IdentityServer4.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using SmartHub.Messaging;
using SmartHub.Messaging.Abstractions;
using SmartHub.Messaging.RabbitMQ;

namespace SmartHub.Identity
{
  public static class ServiceCollectionExtension
  {

    public static void AddIdentityServerCorsPolicy(this IServiceCollection serviceCollection, ICollection<string> allowedOrigins)
    {
      serviceCollection.AddSingleton<ICorsPolicyService>((container) =>
      {
        var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
        return new DefaultCorsPolicyService(logger)
        {
          AllowedOrigins = allowedOrigins
        };
      });
    }

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
