using System;

namespace SmartHub.Identity.Infrastructure.Entities
{
  public class ClientRedirectUri
  {

    public int Id { get; set; }

    public string RedirectUri { get; set; }

    public int ClientId { get; set; }

    public IdentityServerClient Client { get; set; }
  }
}
