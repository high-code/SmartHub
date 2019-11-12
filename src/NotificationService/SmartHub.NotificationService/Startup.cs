using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.NotificationService.Hubs;

namespace SmartHub.NotificationService
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
              // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddLogging();
      services.AddSignalR().AddRedis(
        Configuration["RedisURI"], options => options.Configuration.ClientName = "NotificationService");
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");

        app.UseHsts();
      }

      if (env.IsDevelopment())
      {
        app.UseCors(builder =>
        {
          builder.WithOrigins("https://localhost:44332")
            .AllowAnyHeader()
            .WithMethods("GET", "POST")
            .AllowCredentials();
        });
      }

      app.UseSignalR(builder => builder.MapHub<TelemetryHub>("/telemetry"));
      app.UseHttpsRedirection();
      app.UseStaticFiles();
 
      app.UseMvc();
    }
  }
}
