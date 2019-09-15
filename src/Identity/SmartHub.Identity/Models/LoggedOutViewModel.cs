using System;

namespace SmartHub.Identity.Models
{
  public class LoggedOutViewModel
  {
    public string LogoutId { get; set; }

    public bool AutomaticRedirectAfterSignout { get; set; } = true;
    
    public string PostLogoutRedirectUri { get; set; }

    public string SignoutIframeUrl { get; set; }

    public string ClientName { get; set; }
  }
}
