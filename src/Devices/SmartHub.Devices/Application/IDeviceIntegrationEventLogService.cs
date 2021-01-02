using SmartHub.Messaging.Events;
using System;

using System.Threading.Tasks;

namespace SmartHub.Devices.Application
{
  public interface IDeviceIntegrationEventLogService
  {
    Task PublishEventsAsync(Guid transactionId);
    Task AddEventsAsync(IntegrationEvent evt);
  }
}
