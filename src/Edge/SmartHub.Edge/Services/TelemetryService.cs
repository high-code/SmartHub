using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;
using SmartHub.Edge.Configuration;
using SmartHub.Edge.Contracts;
using SmartHub.Edge.Models;

namespace SmartHub.Edge.Services
{
  public class TelemetryService  : INotificationService
  {

    private readonly KafkaConfiguration _configuration;

    public TelemetryService(KafkaConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async void Save(Telemetry telemetry)
    {

      var producerConf = new ProducerConfig
      {
        BootstrapServers = "kafka:9092"
        
      };

      using (var p = new ProducerBuilder<Null, byte[]>(producerConf).Build())
      {
        try
        {

          var serializedObject = JsonConvert.SerializeObject(telemetry);

          var bytes = Encoding.UTF8.GetBytes(serializedObject);

          var message = new Message<Null, byte[]>() {Value = bytes};
          var delReport = await p.ProduceAsync("telemetry", message);
        }
        catch (ProduceException<Null, Telemetry> e)
        {
          
        }
      }
    }
  }
}
