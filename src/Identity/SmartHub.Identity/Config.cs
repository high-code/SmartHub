using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;

namespace SmartHub.Identity
{
  public static class Config
  {
    public static List<ApiResource> GetAPis()
    {
      return new List<ApiResource>()
      {
        new ApiResource("smarthub", "Smarthub API")
        {
          UserClaims =
          {
            JwtClaimTypes.Name,
            JwtClaimTypes.Subject
          }
        },
        new ApiResource("smarthub.signalr", "Smarthub signalR service")
      };
    }

    public static List<IdentityResource> GetIdentityResources()
    {

      return new List<IdentityResource>
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
      };

    }

    public static List<Client> GetClients(IConfiguration configuration)
    {
      var spaUrl = configuration["SpaUrl"];
      return new List<Client>()
      {
        new Client()
        {
          ClientId = "spa",
          ClientName = "SmartHub spa client",
          AllowAccessTokensViaBrowser = true,
          AllowedGrantTypes = GrantTypes.Implicit,
          RedirectUris =
          {
            $"{spaUrl}/auth.html",
            $"{spaUrl}/silent-renew.html"
          }, // change to configuration setting,
          RequireConsent = false,
          PostLogoutRedirectUris = {$"{spaUrl}"},
          AllowedScopes =
          {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            "smarthub"
            //"smarthub.signalr"
          },
          ClientSecrets =  { new Secret("secret".Sha256())},
          AlwaysIncludeUserClaimsInIdToken = true
        }
      };
    }

    public static List<TestUser> GetTestUsers()
    {
      return new List<TestUser>()
      {
        new TestUser()
        {
          SubjectId = "818727",
          Username = "admin",
          Password = "admin",
          Claims =
          {
            new Claim(JwtClaimTypes.Name, "Andrii Khomenko"),
            new Claim(JwtClaimTypes.GivenName, "Andrii"),
            new Claim(JwtClaimTypes.FamilyName, "Khomenko"),
            new Claim(JwtClaimTypes.Email, "andrii.khomenko@email.com"),
            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            new Claim(JwtClaimTypes.WebSite, "http://andrii.khomenko.com"),
            new Claim(JwtClaimTypes.Address,
              @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }",
              IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
          }
        }
      };
    }

  }
}
