using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartHub.SaverService
{
  class Program
  {
    static void Main(string[] args)
    {

      IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true, true)
        .Build();


      var serviceProvider = new ServiceCollection()
        .AddLogging()
        .AddSingleton<IKafkaConsumer, DatabaseConsumer>()
        .AddScoped<ITelemetryRepository>(provider => new TelemetryRepository(config["connectionString"]));
        


      var servicePro = serviceProvider.BuildServiceProvider();

  
      var databaseConsumer = new DatabaseConsumer();
      var dataService = new DbSaverService(servicePro);
      databaseConsumer.Listen(dataService.SaveData);

    }
  }
}
