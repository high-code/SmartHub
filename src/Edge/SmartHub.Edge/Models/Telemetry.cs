using System;

namespace SmartHub.Edge.Models
{
  public enum TelemetryType
  {
    Temperature,
    Humidity
  }

  public class Telemetry
  {

    public Guid DeviceId { get; set; }

    public TelemetryType Type { get; set; }

    public double Value { get; set; }

  }
}
