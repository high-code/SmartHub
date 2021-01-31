//using IdentityServer4.Models;
//using IdentityServer4.Stores;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SmartHub.Identity.Infrastructure
//{
//  public class EFClientStore : IClientStore
//  {

//    private readonly IdentityServerDbContext _identityServerDbContext;

//    public EFClientStore(IdentityServerDbContext identityServerDbContext)
//    {
//      _identityServerDbContext = identityServerDbContext;
//    }

//    public async Task<Client> FindClientByIdAsync(string clientId)
//    {
//      //var client = await _identityServerDbContext.Clients.FindAsync(clientId);

//      //if (client == null) return null;

//      //var identityServerClient = new Client()
//      //{
//      //  //ClientId = client.ClientId,
//      //  //ClientName = client.ClientName,
//      //  //AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
//      //  //AllowedGrantTypes = client.AllowedGrantTypes.Select(gt => gt.GrantType).ToList(),
//      //  //RedirectUris = client.RedirectUris.Select(ru => ru.RedirectUri).ToList(),
//      //  //RequireConsent = client.RequireConsent,
//      //  //PostLogoutRedirectUris = client.PostLogoutRedirectUris.Select(plru => plru.PostLogoutRedirectUri).ToList(),
//      //  //AllowedScopes = client.AllowedScopes.Select(asc => asc.Scope).ToList(),
//      //  //ClientSecrets = client.ClientSecrets.Select(cs => new Secret(cs.Value)).ToList(),
//      //  //AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken
//      //};
  
//      //return identityServerClient;

//      return null;
//    }
//  }
//}
