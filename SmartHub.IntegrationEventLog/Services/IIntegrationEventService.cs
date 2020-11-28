using SmartHub.Messaging.Events;
using System;
using System.Threading.Tasks;

namespace SmartHub.IntegrationEventLog.Services
{
  public interface IIntegrationEventService
  {
    Task PublishAsync(Guid transactionId);
    Task AddAndSaveEventAsync(IntegrationEvent evt);
  }

}
