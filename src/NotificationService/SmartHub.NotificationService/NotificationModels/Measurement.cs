using System;

namespace SmartHub.NotificationService.NotificationModels
{
  public enum TelemetryType
  {
    Temperature,
    Humidity
  }

  public class Measurement
  {
    public Guid DeviceId { get; set; }

    public TelemetryType Type { get; set; }

    public double Value { get; set; }

  }
}
