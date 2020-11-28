using System;
using Newtonsoft.Json;
using SmartHub.Messaging.Events;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SmartHub.IntegrationEventLog
{
  public class IntegrationEventLogEntry
  {

    public IntegrationEventLogEntry() { }

    public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
    {
      Content = JsonConvert.SerializeObject(@event);
      EventId = @event.Id;
      CreatedTime = @event.CreateDate;
      EventTypeName = @event.GetType().FullName;
      EventState = EventState.NotPublished;
      TransactionId = transactionId;
    }

    public Guid EventId { get; private set; }
    
    public string EventTypeName { get; private set; }

    public EventState EventState { get; set; }

    public DateTime CreatedTime { get; private set; }

    public string Content { get; private set; }


    [NotMapped]
    public string EventTypeShortName => EventTypeName.Split('.')?.Last();

    [NotMapped]
    public IntegrationEvent IntegrationEvent { get; private set; }

    public Guid TransactionId { get; private set; }

    public IntegrationEventLogEntry DeserializeJsonContent(Type type)
    {
      IntegrationEvent = JsonConvert.DeserializeObject(Content, type) as IntegrationEvent;
      return this;
    }
  }
}
