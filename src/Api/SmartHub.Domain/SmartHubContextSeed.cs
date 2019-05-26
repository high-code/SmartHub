using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace SmartHub.Domain
{
  public class SmartHubContextSeed
  {

    public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
    {
      var context = (SmartHubContext) applicationBuilder.ApplicationServices.GetService(typeof(SmartHubContext));

      using (context)
      {
        context.Database.Migrate();


      }
    }
  }
}
