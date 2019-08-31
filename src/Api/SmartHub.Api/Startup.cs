using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartHub.BL.Services;
using SmartHub.BusinessLogic.Contracts;
using SmartHub.Domain;
using SmartHub.Domain.Concrete;
using SmartHub.Domain.Contracts;

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

      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IDeviceService, DeviceService>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {


      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseCors("MyPolicy");

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}


