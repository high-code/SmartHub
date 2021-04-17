using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Identity.Infrastructure.Entities
{
  public class ClientSecret
  {
    public int Id { get; set; }

    public int ClientId { get; set; }

    public IdentityServerClient Client { get; set; }

    public string Value { get; set; }
  }
}
