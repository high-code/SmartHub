using System;
using Microsoft.Extensions.DependencyInjection;

namespace SmartHub.SaverService
{
  public interface IKafkaConsumer
  {
    void Listen(Action<Telemetry> message);
  }
}
