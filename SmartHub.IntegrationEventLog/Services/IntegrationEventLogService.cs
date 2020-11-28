using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using SmartHub.Messaging.Events;

namespace SmartHub.IntegrationEventLog.Services
{
  public class IntegrationEventLogService : IIntegrationEventLogService
  {

    private IntegrationEventLogContext _dbContext;

    List<Type> _eventTypes;

    public IntegrationEventLogService(DbConnection connection)
    {
      _dbContext = new IntegrationEventLogContext(
                new DbContextOptionsBuilder<IntegrationEventLogContext>()
                    .UseNpgsql(connection)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options);


      _eventTypes = Assembly.Load(Assembly.GetEntryAssembly().FullName)
                      .GetTypes()
                      .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
                      .ToList();

    }

    public async Task<IEnumerable<IntegrationEventLogEntry>> GetPendingEventLogEntries(Guid transactionId)
    {

      return await _dbContext.IntegrationEventLogs.Where(e => e.TransactionId == transactionId && e.EventState == EventState.NotPublished)
                                     .OrderBy(o => o.CreatedTime)
                                     .Select(e => e.DeserializeJsonContent(_eventTypes.Find(t => t.Name == e.EventTypeShortName)))
                                     .ToListAsync();

    }

    public Task MarkEventAsFailedAsync(Guid id)
    {
      return UpdateStatus(id, EventState.PublishFailed);
    }

    public Task MarkEventAsInProgressAsync(Guid id)
    {
      return UpdateStatus(id, EventState.InProgress);

    }

    public Task MarkEventAsPublishedAsync(Guid id)
    {
      return UpdateStatus(id, EventState.Published);
    }


    private async Task UpdateStatus(Guid id, EventState state)
    {
      var eventLogEntry = _dbContext.IntegrationEventLogs.Single(i => i.EventId == id);

      eventLogEntry.EventState = state;

      _dbContext.IntegrationEventLogs.Update(eventLogEntry);

      await _dbContext.SaveChangesAsync();
    }

    public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
    {
      var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

      _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
      _dbContext.IntegrationEventLogs.Add(eventLogEntry);

      return _dbContext.SaveChangesAsync();
    }
  }
}
