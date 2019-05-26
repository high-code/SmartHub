using System;
using System.Collections.Generic;
using Confluent.Kafka;
using SmartHub.Edge.Configuration;
using SmartHub.Edge.Models;

namespace SmartHub.Edge.Services
{
  public class TelemetryService
  {

    private readonly KafkaConfiguration _configuration;

    public TelemetryService(KafkaConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async void Save(Telemetry telemetry)
    {

      var producerConf = new ProducerConfig() {BootstrapServers = _configuration.BootstrapServer};

      using (var p = new ProducerBuilder<Null, Telemetry>(producerConf).Build())
      {
        try
        {
          var message = new Message<Null, Telemetry>() {Value = telemetry};
          var delReport = await p.ProduceAsync("telemetry", message);
        }
        catch (ProduceException<Null, Telemetry> e)
        {
          Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
      }
    }
  }
}
