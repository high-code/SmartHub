using SmartHub.Messaging.Abstractions;
using SmartHub.Messaging.Events;
using System;
using System.Collections.Generic;

namespace SmartHub.Messaging.RabbitMQ
{
  public interface IEventBusSubscriptionManager
  {
    bool IsEmpty { get; }

    event EventHandler<string> OnEventRemoved;
    void AddSubscription<T, TH>() where T : IntegrationEvent
                                 where TH : IIntegrationEventHandler<T>;

    void RemoveSubscription<T, TH>() where T : IntegrationEvent
                                     where TH : IIntegrationEventHandler<T>;

    
    void Clear();

    bool HasSubscriptionForEvent<T>() where T : IntegrationEvent;
    bool HasSubscriptionForEvent(string eventName); 
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

    Type GetEventTypeByName(string eventName);

    string GetEventKey<T>();
  }


}
