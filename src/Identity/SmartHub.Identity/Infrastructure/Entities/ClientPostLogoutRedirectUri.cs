namespace SmartHub.Identity.Infrastructure.Entities
{
  public class ClientPostLogoutRedirectUri
  {
    public int Id { get; set; }

    public string PostLogoutRedirectUri { get; set; }

    public int ClientId { get; set; }

    public IdentityServerClient Client { get; set; }
  }
}
