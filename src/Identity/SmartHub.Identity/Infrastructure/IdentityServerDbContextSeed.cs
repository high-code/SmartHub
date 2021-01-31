//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Polly;
//using SmartHub.Identity.Infrastructure.Entities;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SmartHub.Identity.Infrastructure
//{
//  public class IdentityServerDbContextSeed
//  {
//    public async Task SeedAsync(IdentityServerDbContext identityServerDbContext, ILogger<IdentityServerDbContextSeed> logger, IConfiguration configuration)
//    {

//      var retries = 3;
//      var policy = Policy.Handle<SqlException>()
//        .WaitAndRetryAsync(
//        retryCount: retries,
//        sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
//        onRetry: (ex, timeSpan, retry, ctx) =>
//        {
//          logger.LogWarning(ex, "Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", ex.GetType().Name, retry, retries);
//        });


//      var clients = Config.GetClients(configuration).Select(a => new IdentityServerClient()
//      {
//        //ClientId = a.ClientId,
//        //ClientName = a.ClientName,
//        //AllowAccessTokensViaBrowser = a.AllowAccessTokensViaBrowser,
//        //AlwaysIncludeUserClaimsInIdToken = a.AlwaysIncludeUserClaimsInIdToken,
//        //RequireConsent = a.RequireConsent,
//        //PostLogoutRedirectUris = a.PostLogoutRedirectUris.Select(plru => new ClientPostLogoutRedirectUri()
//        //{
//        //  PostLogoutRedirectUri = plru
//        //}).ToList(),
//        //RedirectUris = a.RedirectUris.Select(ru => new ClientRedirectUri()
//        //{
//        //  RedirectUri = ru
//        //}).ToList(),
//        //AllowedGrantTypes = a.AllowedGrantTypes.Select(agt => new ClientGrantType()
//        //{
//        //  GrantType = agt
//        //}).ToList(),
//        //AllowedScopes = a.AllowedScopes.Select(asc => new ClientScope()
//        //{
//        //  Scope = asc
//        //}).ToList(),
//        //ClientSecrets = a.ClientSecrets.Select(cs => new ClientSecret()
//        //{
//        //  Value = cs.Value
//        //}).ToList()
//      });

//      //if (!identityServerDbContext.Clients.Any())
//      //{
//      //  await identityServerDbContext.Clients.AddRangeAsync(clients);
//      //}
//    }
//  }
//}
