using System.Collections.Generic;

namespace SmartHub.Identity.Models
{
  public class DeviceAuthorizationViewModel
  {
    public string ClientId { get; set; }

    public string ClientName { get; set; }

    public string UserCode { get; set; }

    public bool ConfirmUserCode { get; set; }

    public string Button { get; set; }

    public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }

    public IEnumerable<ScopeViewModel> ApiScopes { get; set; }
  }

  public class ScopeViewModel
  {
    public string Value { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public bool Emphasize { get; set; }
    public bool Required { get; set; }
    public bool Checked { get; set; }
  }
}
