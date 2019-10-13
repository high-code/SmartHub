using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SmartHub.BusinessLogic.Contracts;
using SmartHub.BusinessLogic.Services;
using SmartHub.Infrastructure;
using SmartHub.Infrastructure.Concrete;
using SmartHub.Infrastructure.Contracts;

namespace SmartHub.Api
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
      services.AddMvc();

      services.AddEntityFrameworkNpgsql().AddDbContext<SmartHubContext>(opt =>
        opt.UseNpgsql(Configuration["ConnectionString"]));

      services.AddLogging(loggingBuilder => { loggingBuilder.AddSeq(Configuration.GetSection("Seq")); });

      services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
      }));
      
      services.AddAuthentication(options =>
          {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
        .AddJwtBearer(options =>
        {
          options.Authority = Configuration["AuthorityUrl"];
          options.MetadataAddress = "http://smarthub.identity/.well-known/openid-configuration";
          options.RequireHttpsMetadata = false;
          options.Audience = "smarthub";
          options.IncludeErrorDetails = true;
          options.TokenValidationParameters = new TokenValidationParameters()
          {
            NameClaimType = ClaimTypes.NameIdentifier
          };

        });
      
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IDeviceService, DeviceService>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseCors("MyPolicy");
      app.UseAuthentication();
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }


      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}


