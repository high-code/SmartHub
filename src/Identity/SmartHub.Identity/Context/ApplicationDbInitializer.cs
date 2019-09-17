using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHub.Identity.Identity;

namespace SmartHub.Identity.Context
{
  public static class ApplicationDbInitializer
  {

    public static void SeedUsers(UserManager<ApplicationUser> userManager)
    {
      if (userManager.FindByEmailAsync("admin@smarthub.com").Result == null)
      {
        ApplicationUser user = new ApplicationUser()
        {
          UserName = "admin",
          Email = "admin@smarthub.com"
        };

        var result = userManager.CreateAsync(user, "A^dmin132").Result;

        if (result.Succeeded)
        {
          userManager.AddToRoleAsync(user, "Admin").Wait();
        }
      }
    }
  }
}
