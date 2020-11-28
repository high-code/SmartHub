using Microsoft.EntityFrameworkCore;
using SmartHub.Edge.Infrastructure;
using SmartHub.IntegrationEventLog.Services;
using SmartHub.Messaging.Abstractions;
using SmartHub.Messaging.Events;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SmartHub.Edge.Application.IntegrationEvents
{
  public class EdgeIntegrationEventService : IEdgeIntegrationEventLogService
  {
    private readonly IEventBus _eventBus;
    private readonly IIntegrationEventLogService _integrationEventLogService;
    private readonly EdgeDbContext _edgeContext;

    public EdgeIntegrationEventService(IEventBus eventBus, EdgeDbContext edgeContext,
                                        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)

    {
      _eventBus = eventBus;
      _edgeContext = edgeContext;
      _integrationEventLogService = integrationEventLogServiceFactory(_edgeContext.Database.GetDbConnection());
    }

    public async Task PublishEventsAsync(Guid transactionId)
    {

      var pendingEventLogs = await _integrationEventLogService.GetPendingEventLogEntries(transactionId);

      foreach (var eventLog in pendingEventLogs)
      {
        try
        {
          await _integrationEventLogService.MarkEventAsInProgressAsync(eventLog.EventId);
          _eventBus.Publish(eventLog.IntegrationEvent);
          await _integrationEventLogService.MarkEventAsPublishedAsync(eventLog.EventId);
        }
        catch (Exception ex)
        {
          Debug.WriteLine("Error publishing");
          await _integrationEventLogService.MarkEventAsFailedAsync(eventLog.EventId);
        }
      }

    }

    public async Task AddEventsAsync(IntegrationEvent integrationEvent)
    {
      await _integrationEventLogService.SaveEventAsync(integrationEvent, _edgeContext.Database.CurrentTransaction);

    }
  }
}
