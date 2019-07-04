using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SmartHub.KafkaConsumer;

namespace SmartHub.NotificationService
{
  public class ConsumerHostedService : IHostedService
  {

    private readonly IKafkaConsumer _kafkaConsumer;
    private readonly INotificationService _notificationService;

    public ConsumerHostedService(IKafkaConsumer consumer, INotificationService notificationService)
    {
      _kafkaConsumer = consumer;
      _notificationService = notificationService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {

      var callbacksDictionary = new Dictionary<string, Action<string>>();
      callbacksDictionary.Add("telemetry", _notificationService.SendMeasurementNotification);
      callbacksDictionary.Add("status", _notificationService.SendStatusNotification);

      return Task.Run(() =>
      {
        _kafkaConsumer.Listen(callbacksDictionary, new [] {"telemetry", "status"}, cancellationToken);

      }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}
