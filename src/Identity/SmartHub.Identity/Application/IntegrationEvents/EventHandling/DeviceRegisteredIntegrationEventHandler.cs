using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using SmartHub.DataAccess.Contracts;
using SmartHub.Identity.Application.IntegrationEvents.Events;
using SmartHub.Identity.Infrastructure;
using SmartHub.Identity.Infrastructure.Entities;
using SmartHub.Messaging.Abstractions;

namespace SmartHub.Identity.Application.IntegrationEvents
{
  public class DeviceRegisteredIntegrationEventHandler : IIntegrationEventHandler<DeviceRegisteredIntegrationEvent>
  {

    private readonly IRepository<IdentityServerDbContext,IdentityServerClient> _repository;

    public DeviceRegisteredIntegrationEventHandler(IRepository<IdentityServerDbContext, IdentityServerClient> repository)
    {
      _repository = repository;
    }

    public Task Handle(DeviceRegisteredIntegrationEvent integrationEvent)
    {
      var client = new IdentityServerClient()
      {
        ClientId = integrationEvent.Id.ToString(),
        ClientName = integrationEvent.Name.ToString(),
        UserId = integrationEvent.UserCreated,
        AllowAccessTokensViaBrowser = true,
        AllowedGrantTypes = GrantTypes.DeviceFlow.Select(agt => new ClientGrantType()
        {
          GrantType = agt
        }).ToList(),
        RequireConsent = true,
        AllowedScopes = new List<ClientScope>()
        {
          new ClientScope()
          {
            Scope = IdentityServerConstants.StandardScopes.OpenId,
          },
          new ClientScope()
          {
            Scope = IdentityServerConstants.StandardScopes.Profile,
          },
          new ClientScope()
          {
            Scope = "smarthub"
          }
        },
        ClientSecrets = new List<ClientSecret>
        {
          new ClientSecret()
          {
            Value = "device".ToSha256()
          }
        }
      };
      _repository.Add(client);
      _repository.Context.SaveChanges();
      return Task.CompletedTask;
    }
  }
}
