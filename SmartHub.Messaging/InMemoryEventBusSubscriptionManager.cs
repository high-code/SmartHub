using SmartHub.Messaging.Abstractions;
using SmartHub.Messaging.Events;
using SmartHub.Messaging.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHub.Messaging
{
  public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
  {

    private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
    private readonly List<Type> _eventTypes;

    public InMemoryEventBusSubscriptionManager()
    {
      _handlers = new Dictionary<string, List<SubscriptionInfo>>();
      _eventTypes = new List<Type>();
    }

    public bool IsEmpty => _handlers.Keys.Any();
    public void Clear() { _handlers.Clear(); }

    public event EventHandler<string> OnEventRemoved;

    public void AddSubscription<T, TH>()
      where T : IntegrationEvent
      where TH : IIntegrationEventHandler<T>
    {
      var eventName = GetEventKey<T>();

      DoAddSubscription(typeof(TH), eventName);

      if(!_eventTypes.Contains(typeof(T)))
      {
        _eventTypes.Add(typeof(T));
      }
    }

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
    {
      var eventName = GetEventKey<T>();

      return GetHandlersForEvent(eventName);
    }

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];




    public void RemoveSubscription<T, TH>()
      where T : IntegrationEvent
      where TH : IIntegrationEventHandler<T>
    {
      var handlerToRemove = FindSubscriptionToRemove<T, TH>();
      var eventName = GetEventKey<T>();
      DoRemoveHandler(eventName, handlerToRemove);
    }


    private void DoAddSubscription(Type handlerType, string eventName)
    {
      if(!HasSubscriptionForEvent(eventName))
      {
        _handlers.Add(eventName, new List<SubscriptionInfo>());
      }

      if(_handlers[eventName].Any(s => s.HandlerType == handlerType))
      {
        throw new ArgumentException("Handler type is already registered");
      }
      _handlers[eventName].Add(new SubscriptionInfo(handlerType));
    }

    private void RaiseOnEventRemoved(string eventName)
    {
      var handler = OnEventRemoved;
      handler?.Invoke(this, eventName);
    }

    private void DoRemoveHandler(string eventName, SubscriptionInfo toRemove)
    {
      if(toRemove != null)
      {
        _handlers[eventName].Remove(toRemove);

        if(!_handlers[eventName].Any())
        {
          _handlers.Remove(eventName);
          var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);

          if(eventType != null)
          {
            _eventTypes.Remove(eventType);
          }
          RaiseOnEventRemoved(eventName);
        }
      }
    }

    private SubscriptionInfo FindSubscriptionToRemove<T, TH>()
      where T : IntegrationEvent
      where TH : IIntegrationEventHandler<T>
    {
      var eventName = GetEventKey<T>();

      return DoFindSubscriptionToRemove(eventName, typeof(TH));
    }

    private SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType)
    {
      if(!HasSubscriptionForEvent(eventName))
      {
        return null;
      }

      return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
    }

    public bool HasSubscriptionForEvent<T>() where T : IntegrationEvent
    {
      var key = GetEventKey<T>();

      return HasSubscriptionForEvent(key);
    }

    public bool HasSubscriptionForEvent(string eventName) => _handlers.ContainsKey(eventName);

    public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

    public string GetEventKey<T>() => typeof(T).Name;
  }
}
