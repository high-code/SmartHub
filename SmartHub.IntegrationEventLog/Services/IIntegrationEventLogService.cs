using Microsoft.EntityFrameworkCore.Storage;
using SmartHub.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.IntegrationEventLog.Services
{
  public interface IIntegrationEventLogService
  {

    Task<IEnumerable<IntegrationEventLogEntry>> GetPendingEventLogEntries(Guid transactionId);

    Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);

    Task MarkEventAsPublishedAsync(Guid id);

    Task MarkEventAsInProgressAsync(Guid id);

    Task MarkEventAsFailedAsync(Guid id);
  }
}
