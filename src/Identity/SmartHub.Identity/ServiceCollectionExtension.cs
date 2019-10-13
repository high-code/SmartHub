using System;
using System.Collections.Generic;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartHub.Identity
{
  public static class ServiceCollectionExtension
  {

    public static void AddIdentityServerCorsPolicy(this IServiceCollection serviceCollection, ICollection<string> allowedOrigins, ILoggerFactory loggerFactory)
    {
      var cors = new DefaultCorsPolicyService(loggerFactory.CreateLogger<DefaultCorsPolicyService>())
      {
        AllowedOrigins = allowedOrigins
      };
      serviceCollection.AddSingleton<ICorsPolicyService>(cors);
    }

  }
}
