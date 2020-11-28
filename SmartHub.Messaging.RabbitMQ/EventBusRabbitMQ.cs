using Autofac;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using SmartHub.Messaging.Abstractions;
using SmartHub.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Messaging.RabbitMQ
{
  public class EventBusRabbitMQ : IEventBus, IDisposable
  {
    const string BROKER_NAME = "smarthub_eventbus";

    private ILifetimeScope _scope;
    private IRabbitMQPersistentConnection _persistentConnection;
    private readonly IEventBusSubscriptionManager _subscriptionManager;
    private readonly ILogger _logger;
    private IModel _consumerChannel;

    private int _retryCount;
    private readonly string AUTOFAC_SCOPE_NAME = "smarthub_eventbus";
    private string _queueName;

    public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, IEventBusSubscriptionManager subscriptionManager,
      ILifetimeScope scope, ILogger logger, int retryCount, string queueName)
    {
      _scope = scope;
      _persistentConnection = persistentConnection;
      _subscriptionManager = subscriptionManager ?? new InMemoryEventBusSubscriptionManager();
      _logger = logger;
      _scope = scope;
      _retryCount = retryCount;
      _queueName = queueName;
      _consumerChannel = CreateConsumerChannel();

    }

    private void SubsManager_OnEventRemoved(object sender, string eventName)
    {
      if(!_persistentConnection.IsConnected)
      {
        _persistentConnection.TryConnect();
      }

      using(var channel = _persistentConnection.CreateModel())
      {
        channel.QueueUnbind(_queueName,
                      BROKER_NAME,
                      eventName);

        if(_subscriptionManager.IsEmpty)
        {
          _queueName = string.Empty;
          _consumerChannel.Close();
        }
      }
    }

    public void Publish(IntegrationEvent @event)
    {
      if (!_persistentConnection.IsConnected)
      {
        _persistentConnection.TryConnect();
      }

      var policy = Policy.Handle<BrokerUnreachableException>()
        .Or<SocketException>()
        .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
        (ex, time) =>
        {
          Debug.WriteLine("Could not publish event");
        });

      var eventName = @event.GetType().Name;

      using (var channel = _persistentConnection.CreateModel())
      {
        channel.ExchangeDeclare(BROKER_NAME, "direct");

        var message = JsonConvert.SerializeObject(@event);

        var body = Encoding.UTF8.GetBytes(message);

        policy.Execute(() =>
        {
          var properties = channel.CreateBasicProperties();
          properties.DeliveryMode = 2;

          channel.BasicPublish(
            BROKER_NAME,
            eventName,
            true,
            properties,
            body);
        });
      }


    }

    public void Subscribe<T, TH>()
      where T : IntegrationEvent
      where TH : IIntegrationEventHandler<T>
    {
      var eventName = _subscriptionManager.GetEventKey<T>();
      DoInternalSubscription(eventName);
      _subscriptionManager.AddSubscription<T, TH>();
      StartBasicConsume();
    }

    public void Unsubscribe<T, TH>()
      where T : IntegrationEvent
      where TH : IIntegrationEventHandler<T>
    {

      var eventName = _subscriptionManager.GetEventKey<T>();

      _subscriptionManager.RemoveSubscription<T, TH>();
    }

    private void DoInternalSubscription(string eventName)
    {
      var containsKey = _subscriptionManager.HasSubscriptionForEvent(eventName);

      if (!containsKey)
      {
        if (!_persistentConnection.IsConnected)
        {
          _persistentConnection.TryConnect();
        }

        using (var channel = _persistentConnection.CreateModel())
        {
          channel.QueueBind(queue: _queueName,
                            exchange: BROKER_NAME,
                            routingKey: eventName);
        }
      }

    }

    private void StartBasicConsume()
    {
      if (_consumerChannel != null)
      {
        var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

        consumer.Received += Consumer_Receive;

        _consumerChannel.BasicConsume(
          queue: _queueName,
          autoAck: false,
          consumer: consumer);
      }
    }


    private async Task Consumer_Receive(object sender, BasicDeliverEventArgs eventArgs)
    {
      var eventName = eventArgs.RoutingKey;
      var message = Encoding.UTF8.GetString(eventArgs.Body);

      try
      {
        await ProcessEvent(eventName, message);
      }
      catch (Exception ex)
      {

        throw;
      }

      _consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
    }

    private IModel CreateConsumerChannel()
    {
      if (!_persistentConnection.IsConnected)
      {
        _persistentConnection.TryConnect();
      }

      var channel = _persistentConnection.CreateModel();

      channel.ExchangeDeclare(BROKER_NAME, "direct");

      channel.QueueDeclare(_queueName, true, false, false, null);

      channel.CallbackException += (sender, ea) =>
      {
        _consumerChannel.Dispose();
        _consumerChannel = CreateConsumerChannel();
        StartBasicConsume();
      };


      return channel;
    }

    private async Task ProcessEvent(string eventName, string message)
    {

      if (_subscriptionManager.HasSubscriptionForEvent(eventName))
      {
        using (var scope = _scope.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
        {
          var subscriptions = _subscriptionManager.GetHandlersForEvent(eventName);

          foreach (var subscription in subscriptions)
          {
            var handler = scope.ResolveOptional(subscription.HandlerType);
            if (handler == null) return;

            var eventType = _subscriptionManager.GetEventTypeByName(eventName);

            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);

            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
          }
        }
      }
    }


    public void Dispose()
    {
      if (_consumerChannel != null)
      {
        _consumerChannel.Dispose();
      }

      _subscriptionManager.Clear();
    }
  }
}
