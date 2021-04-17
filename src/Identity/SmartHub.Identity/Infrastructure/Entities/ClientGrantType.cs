namespace SmartHub.Identity.Infrastructure.Entities
{
  public class ClientGrantType
  {
    public int Id { get; set; }

    public string GrantType { get; set; }

    public int ClientId { get; set; }

    public IdentityServerClient Client { get; set; }
  }
}
