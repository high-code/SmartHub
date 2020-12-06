using System;

namespace SmartHub.Edge.Domain.MeasurementAggregate
{
  public class Measurement
  {
    public Measurement()
    {

    }

    public Measurement(Guid deviceId, DateTime dtSent,DateTime dtReceived, double value, int measurementType)
    {
      DeviceId = deviceId;
      DtSent = dtSent;
      DtReceived = dtReceived;
      Value = value;
      Type = (MeasurementType)measurementType;
      DtReceived = DateTime.UtcNow;
    }

    public int Id { get; set; }

    public Guid DeviceId { get; set; }

    public DateTime DtSent { get; set; }

    public DateTime DtReceived { get; set; }

    public double Value { get; set; }

    public MeasurementType Type { get; set; }

  }
}
