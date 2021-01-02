using SmartHub.Messaging.Events;
using System;

namespace SmartHub.Devices.Application.IntegrationEvents
{
  public class DeviceRegisteredIntegrationEvent : IntegrationEvent
  {
    public Guid DeviceId { get; }

    public Guid UserCreated { get; }

    public string Name { get; }

    public string Description { get; }

    public DeviceRegisteredIntegrationEvent(Guid deviceId, Guid userId, string name, string description)
    {
      DeviceId = deviceId;
      UserCreated = userId;
      Name = name;
      Description = description;
    }
  }
}
