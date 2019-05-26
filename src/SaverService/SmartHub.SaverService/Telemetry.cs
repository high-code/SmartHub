namespace SmartHub.SaverService
{
  public enum TelemetryType
  {
    Temperature,
    Humidity
  }

  public class Telemetry
  {
    public int DeviceId { get; set; }

    public TelemetryType Type { get; set; }

    public double Value { get; set; }

  }
}
