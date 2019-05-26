using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Confluent.Kafka;
using Newtonsoft.Json;


namespace SmartHub.SaverService
{
  public class DatabaseConsumer : IKafkaConsumer
  {

    public void Listen(Action<Telemetry> consume)
    {

      var conf = new ConsumerConfig
      {
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = AutoOffsetReset.Earliest,
        GroupId = "test-group",
      };


      using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
      {

        c.Subscribe("telemetry");

        try
        {
          while (true)
          {
            try
            {
              var cr = c.Consume();
              consume(JsonConvert.DeserializeObject<Telemetry>(cr.Value));

            }
            catch (ConsumeException e)
            {
              Console.WriteLine($"Error occured: {e.Error.Reason}");
            }
          }
        }
        catch (Exception)
        {
          c.Close();
        }
      }


    }
  }
}
