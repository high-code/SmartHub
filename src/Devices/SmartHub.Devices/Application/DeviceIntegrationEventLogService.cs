using System;
using System.Data.Common;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHub.Infrastructure;
using SmartHub.IntegrationEventLog.Services;
using SmartHub.Messaging.Abstractions;
using SmartHub.Messaging.Events;

namespace SmartHub.Devices.Application
{
  public class DeviceIntegrationEventLogService : IDeviceIntegrationEventLogService
  {
    private readonly IEventBus _eventBus;
    private readonly IIntegrationEventLogService _integrationEventLogService;
    private readonly SmartHubContext _smarthubContext;

    public DeviceIntegrationEventLogService(IEventBus eventBus, SmartHubContext smarthubContext,
                                        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)

    {
      _eventBus = eventBus;
      _smarthubContext = smarthubContext;
      _integrationEventLogService = integrationEventLogServiceFactory(_smarthubContext.Database.GetDbConnection());
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
      await _integrationEventLogService.SaveEventAsync(integrationEvent, _smarthubContext.Database.CurrentTransaction);

    }
  }
}
