using SmartHub.Messaging.Events;
using System;

namespace SmartHub.Identity.Application.IntegrationEvents.Events
{
  public class DeviceRegisteredIntegrationEvent : IntegrationEvent
  {
    public Guid DeviceId { get; }

    public Guid UserCreated { get; }

    public string Name { get; }

    public string Description { get; }

    public DeviceRegisteredIntegrationEvent(Guid deviceId, Guid userCreated, string name, string description)
    {
      DeviceId = deviceId;
      UserCreated = userCreated;
      Name = name;
      Description = description;
    }
  }
}
