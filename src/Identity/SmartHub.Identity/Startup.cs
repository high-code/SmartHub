using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartHub.Identity.Context;
using SmartHub.Identity.Identity;

namespace SmartHub.Identity
{
  public class Startup
  {
    public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
      Configuration = configuration;
      _loggerFactory = loggerFactory;
    }

    public IConfiguration Configuration { get; }

    private ILoggerFactory _loggerFactory;

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddEntityFrameworkNpgsql().AddDbContext<SmartHubIdentityDbContext>(o =>
      {
        o.UseNpgsql(Configuration.GetConnectionString("Default"));
      });

      // add identity
      services.AddIdentity<ApplicationUser,IdentityRole>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<SmartHubIdentityDbContext>()
        .AddDefaultTokenProviders();

      services.AddIdentityServer(
          option => { option.IssuerUri = "devenv"; })
        .AddInMemoryClients(Config.GetClients(Configuration))
        .AddInMemoryApiResources(Config.GetAPis())
        .AddInMemoryIdentityResources(Config.GetIdentityResources())
        .AddDeveloperSigningCredential()
        .AddAspNetIdentity<ApplicationUser>();

      
      
      services.AddIdentityServerCorsPolicy(new List<string>{ Configuration["SpaUrl"] }, _loggerFactory);
      services.AddCors(o => o.AddPolicy("SpaAuthCors", builder =>
      {
        builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
      }));


      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager)
    {

      app.UseStaticFiles();
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      // seed with default user
      if (env.IsDevelopment())
      {
        ApplicationDbInitializer.SeedUsers(userManager);
      }

      app.UseAuthentication();
      app.UseCors("SpaAuthCors");
      app.UseStaticFiles();
      app.UseCookiePolicy();
      app.UseIdentityServer();
      app.UseMvcWithDefaultRoute();
    }


  }
}
