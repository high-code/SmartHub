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
using SmartHub.Identity.Infrastructure;
using SmartHub.Identity;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace SmartHub.Edge
{
  public class Program
  {
    public const string AppName = "SmartHub.Identity";
    public static int Main(string[] args)
    {

      var configuration = GetConfiguration();
      Log.Logger = CreateLogger(configuration);

      try
      {
        Log.Information("Configure and start application {ApplicationContext}", AppName);
        var webHost = CreateWebHostBuilder(configuration, args).Build();

        Log.Information("Applying migration {ApplicationContext}", AppName);
        webHost.MigrateDbContext<IdentityServerDbContext>((context, services) =>
        {
          var logger = services.GetService<ILogger<IdentityServerDbContextSeed>>();
          var dbContext = services.GetService<IdentityServerDbContext>();
          new IdentityServerDbContextSeed().SeedAsync(dbContext, logger, configuration).Wait();
        });


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
    public static Serilog.ILogger CreateLogger(IConfiguration configuration)
    {
      var seqServerUrl = configuration["Serilog:SeqServerUrl"];
      return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApplicationContext", AppName)
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
        .WriteTo.Seq(seqServerUrl)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
    }
  }

  public static class WebHostExtensions
  {
    public static IWebHost MigrateDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
    {
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<TContext>>();

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
          seeder(context, services);
        });

        return host;
      }
    }
  }
}
