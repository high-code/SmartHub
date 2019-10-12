using System;
using Microsoft.AspNetCore.Identity;

namespace SmartHub.Identity.Identity
{
  public class ApplicationUser : IdentityUser
  {
    public string FullName { get; set; }
  }
}
