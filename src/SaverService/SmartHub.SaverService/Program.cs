using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartHub.KafkaConsumer;
using SmartHub.SaverService.Concrete;
using SmartHub.SaverService.DbEntities;
using SmartHub.SaverService.DTO;

namespace SmartHub.SaverService
{
  class Program
  {
    private static IConfiguration _configuration;

    static void Main(string[] args)
    {

      _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", false, false)
        .AddEnvironmentVariables()
        .Build();

      var serviceCollection = new ServiceCollection()
        .AddLogging()
        .AddSingleton<IStorageService, DbStorageService>()
        .AddSingleton<IKafkaConsumer, Consumer>(i => new Consumer(i.GetRequiredService<ILogger<Consumer>>(), new ConsumerConfig
        {
          BootstrapServers = _configuration["BrokerURI"],
          GroupId = _configuration["KafkaConsumerGroupId"],
          AutoOffsetReset = AutoOffsetReset.Earliest
        }))
        .AddScoped<IRepository<DbMeasurement>>(provider => new StorageRepository(_configuration["ConnectionString"]))
        .AddScoped<IRepository<DbStatus>>(provider => new StorageRepository(_configuration["ConnectionString"]));

      using (var serviceProvider = serviceCollection.BuildServiceProvider())
      {
        var databaseConsumer = serviceProvider.GetRequiredService<IKafkaConsumer>();
        var storageService = serviceProvider.GetRequiredService<IStorageService>();

        var callbacksDictionary = new Dictionary<string, Action<string>>();

        callbacksDictionary.Add("telemetry", storageService.StoreMeasurements);
        callbacksDictionary.Add("status", storageService.StoreStatus);
    
        databaseConsumer.Listen(callbacksDictionary, new [] {"telemetry", "status"});
      }
    }
  }
}
