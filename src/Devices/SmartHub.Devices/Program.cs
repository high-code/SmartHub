using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace SmartHub.Api
{
  public class Program
  {

    public const string AppName = "Smarthub.Devices";
    public static int Main(string[] args)
    {
      var configuration = GetConfiguration();
      Log.Logger = CreateLogger(configuration);

      try
      {
        Log.Information("Configure and start application {ApplicationContext}", AppName);
        CreateWebHostBuilder(configuration, args).Build().Run();
        Log.Information("{ApplicationContext} statrted successfully", AppName);
        return 0;
      }
      catch(Exception ex)
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
}
