using SmartHub.Messaging.Events;
using System;
using System.Threading.Tasks;

namespace SmartHub.Edge.Application.IntegrationEvents
{
  public interface IEdgeIntegrationEventLogService
  {
    Task PublishEventsAsync(Guid transactionId);
    Task AddEventsAsync(IntegrationEvent evt);
  }
}
