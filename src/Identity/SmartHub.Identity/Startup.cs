using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartHub.Identity.Application.IntegrationEvents;
using SmartHub.Identity.Application.IntegrationEvents.Events;
using SmartHub.Identity.Context;
using SmartHub.Identity.Identity;
using SmartHub.Identity.Infrastructure;
using SmartHub.Identity.Infrastructure.Modules;
using SmartHub.Messaging.Abstractions;

namespace SmartHub.Identity
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddLogging(
           loggingBuilder => loggingBuilder.AddSeq(Configuration.GetSection("Seq")))
           .AddCustomIntegrations(Configuration)
           .AddEventBus(Configuration);

      services.AddEntityFrameworkNpgsql().AddDbContext<SmartHubIdentityDbContext>(o =>
      {
        o.UseNpgsql(Configuration.GetConnectionString("Default"));
      });

      services.AddEntityFrameworkNpgsql().AddDbContext<IdentityServerDbContext>(o =>
      {
        o.UseNpgsql(Configuration.GetConnectionString("Default"),
          sqlOptions =>
          {
            sqlOptions.MigrationsAssembly("SmartHub.Identity");
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
          });
      }, ServiceLifetime.Scoped);

      // add identity
      services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<SmartHubIdentityDbContext>()
        .AddDefaultTokenProviders();

      services.AddIdentityServer(
          option => { option.IssuerUri = "https://localhost:5001"; })
        //.AddInMemoryClients(Config.GetClients(Configuration))
        .AddInMemoryApiResources(Config.GetAPis())
        .AddClientStore<EFClientStore>()
        .AddInMemoryIdentityResources(Config.GetIdentityResources())
        .AddDeveloperSigningCredential()
        .AddAspNetIdentity<ApplicationUser>();



      services.AddIdentityServerCorsPolicy(new List<string> { Configuration["SpaUrl"] });
      services.AddCors(o => o.AddPolicy("SpaAuthCors", builder =>
      {
        builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
      }));

      services.AddMvc();

      var container = new ContainerBuilder();
      container.RegisterModule(new ApplicationModule());
      container.Populate(services);


      return new AutofacServiceProvider(container.Build());
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
      ConfigureEventBus(app);
    }

    private void ConfigureEventBus(IApplicationBuilder app)
    {
      var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

      eventBus.Subscribe<DeviceRegisteredIntegrationEvent, DeviceRegisteredIntegrationEventHandler>();

    }
  }
}
