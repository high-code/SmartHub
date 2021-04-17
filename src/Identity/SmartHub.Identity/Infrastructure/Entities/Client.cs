using System;
using System.Collections.Generic;

namespace SmartHub.Identity.Infrastructure.Entities
{
  public class IdentityServerClient
  {
    public int Id { get; set; }

    public string ClientId { get; set; }

    public Guid UserId { get; set; }

    public string ClientName { get; set; }

    public bool AllowAccessTokensViaBrowser { get; set; }

    public virtual List<ClientGrantType> AllowedGrantTypes { get; set; }

    public virtual List<ClientRedirectUri> RedirectUris { get; set; }

    public bool RequireConsent { get; set; } = false;

    public virtual List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

    public virtual List<ClientScope> AllowedScopes { get; set; }

    public virtual List<ClientSecret> ClientSecrets { get; set; }

    public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
  }
}
