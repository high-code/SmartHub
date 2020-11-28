using SmartHub.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Messaging.Abstractions
{
  public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent: IntegrationEvent
  {
    Task Handle(TIntegrationEvent integrationEvent);
  }

  public interface IIntegrationEventHandler
  {

  }
}
