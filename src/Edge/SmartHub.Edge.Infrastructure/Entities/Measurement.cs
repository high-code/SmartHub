using System;

namespace SmartHub.Edge.Infrastructure.Entities
{
  public class Measurement
  {
    public int Id { get; set; }

    public int DeviceId { get; set; }

    public DateTime DtSent { get; set; }

    public double Value { get; set; }

    public MeasurementType Type { get; set; }

  }
}
