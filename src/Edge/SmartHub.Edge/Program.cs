using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Serilog;
using SmartHub.Edge.Infrastructure;

namespace SmartHub.Edge
{
  public class Program
  {
    public const string AppName = "SmartHub.Edge";
    public static int Main(string[] args)
    {

      var configuration = GetConfiguration();
      Log.Logger = CreateLogger(configuration);

      try
      {
        Log.Information("Configure and start application {ApplicationContext}", AppName);
        var webHost = CreateWebHostBuilder(configuration, args).Build();

        Log.Information("Applying migration {ApplicationContext}",AppName);
        webHost.MigrateDbContext<EdgeDbContext>();


        Log.Information("{ApplicationContext} started successfully", AppName);
        webHost.Run();
        return 0;
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Host terminated unexpectedly");
        return 1;
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(IConfiguration configuration, string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseConfiguration(configuration)
            .UseSerilog();


    public static IConfiguration GetConfiguration()
    {
      return new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
    }
    public static ILogger CreateLogger(IConfiguration configuration)
    {
      var seqServerUrl = configuration["Serilog:SeqServerUrl"];
      return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApplicationContext", AppName)
        .WriteTo.Console()
        .WriteTo.Seq(seqServerUrl)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
    }
  }

  public static class WebHostExtensions
  {
    public static IWebHost MigrateDbContext<TContext>(this IWebHost host) where TContext : DbContext
    {
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;

        var context = services.GetService<TContext>();

        var retry = Policy.Handle<SqlException>()
                    .WaitAndRetry(new TimeSpan[]
                     {
                             TimeSpan.FromSeconds(3),
                             TimeSpan.FromSeconds(5),
                             TimeSpan.FromSeconds(8),
                     });

        retry.Execute(() =>
        {
          context.Database.Migrate();
        });

        return host;
      }
    }
  }
}
