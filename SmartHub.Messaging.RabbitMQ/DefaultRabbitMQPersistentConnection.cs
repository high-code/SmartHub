using System;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client.Events;
using System.IO;

namespace SmartHub.Messaging.RabbitMQ
{
  public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
  {

    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;

    private IConnection _connection;

    private readonly int _retryCount = 5;


    public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;


    object _syncRoot = new object();

    private bool _disposed;

    public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogger<DefaultRabbitMQPersistentConnection> logger,int retryCount = 5)
    {

      _retryCount = retryCount;
      _connectionFactory = connectionFactory;
      _logger = logger;
    }
    

    public IModel CreateModel()
    {
      if(!IsConnected)
      {
        throw new InvalidOperationException("RabbitMQ exception is not established to create model");
      }

      return _connection.CreateModel();
    }

    public void Dispose()
    {
      if (_disposed) return;

      _disposed = true;

      try
      {
        _connection.Dispose();
      }
      catch(IOException ex)
      {
        _logger.LogCritical(ex.ToString());
      }

    }

    public bool TryConnect()
    {
      lock (_syncRoot)
      {
        var policy = Policy.Handle<SocketException>()
                      .Or<BrokerUnreachableException>()
                      .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                      {
                        _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                      });

        policy.Execute(() =>
        {
          _connection = _connectionFactory.CreateConnection();
        });

        if (IsConnected)
        {
          _connection.ConnectionShutdown += OnConnectionShutdown;
          _connection.CallbackException += OnCallbackException;
          _connection.ConnectionBlocked += OnConnectionBlocked;

          return true;
        }
        else
        {
          return false;
        }

      }
    }

    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs args)
    {
      if (_disposed) return;

      _logger.LogWarning("Connection was blocked, trying to reconnect");

      TryConnect();
    }

    private void OnCallbackException(object sender, CallbackExceptionEventArgs args)
    {
      if (_disposed) return;

      _logger.LogWarning("Callback exception happened, trying to reconnect");

      TryConnect();
    }

    private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
      if (_disposed) return;

      _logger.LogWarning("RabbitMQ connection was closed, trying to reconnect");

      TryConnect();

    }

  }
}
