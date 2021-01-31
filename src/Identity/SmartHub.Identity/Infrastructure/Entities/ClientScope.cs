using System;
using System.Collections.Generic;

namespace SmartHub.Identity.Infrastructure.Entities
{
  public class ClientScope
  {
    public int Id { get; set; }

    public string Scope { get; set; }

    public int ClientId { get; set; }

    public IdentityServerClient Client { get; set; }
  }
}
