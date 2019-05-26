using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace SmartHub.NotificationService
{
  public class TelemetryConsumerService : IHostedService
  {

    private readonly IServiceProvider _serviceProvider;

    public TelemetryConsumerService(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {

      var consumerConfig = new ConsumerConfig
      {
        GroupId = "notification-consumers"
      };

      var t = new Task(() =>
      {
        using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
        {


          try
          {
            while (true)
            {
              try
              {
                var consumeResult = consumer.Consume(cancellationToken);

                var deserializedResult = JsonConvert.DeserializeObject<Telemetry>(consumeResult.Value);
                using (var scope = _serviceProvider.CreateScope())
                {
                  var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                  notificationService.Notify(deserializedResult);

                }

              }
              catch (ConsumeException ex)
              {

              }
            }
          }
          catch (Exception e)
          {
            consumer.Close();
          }




        }
      }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
