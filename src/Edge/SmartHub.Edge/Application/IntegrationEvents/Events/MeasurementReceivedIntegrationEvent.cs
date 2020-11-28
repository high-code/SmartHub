using SmartHub.Messaging.Events;
using System;

namespace SmartHub.Edge.Application.IntegrationEvents.Events
{
  public class MeasurementReceivedIntegrationEvent : IntegrationEvent
  {

    public int DeviceId { get; }

    public DateTime DtSent { get; }

    public int MeasurementType { get; }

    public double Value { get; }

    public MeasurementReceivedIntegrationEvent(int deviceId, DateTime sent,
                                               int measurementType, double value)
    {
      DeviceId = deviceId;
      DtSent = sent;
      MeasurementType = measurementType;
      Value = value;
    }
  }
}
