using System.Collections.Generic;
using SmartHub.Edge.Application.DTO;
using SmartHub.Messaging.Events;

namespace SmartHub.Edge.Application.IntegrationEvents.Events
{
  public class MeasurementReceivedIntegrationEvent : IntegrationEvent
  {

    public IEnumerable<MeasurementDTO> Measurements { get; }

    public MeasurementReceivedIntegrationEvent(IEnumerable<MeasurementDTO> measurements)
    {
      Measurements = measurements;
    }
  }

}
