using System;

namespace SmartHub.Edge.Domain.MeasurementAggregate
{
  public class Measurement
  {
    public Measurement()
    {

    }

    public Measurement(int deviceId, DateTime dtSent, double value, int measurementType)
    {
      DeviceId = deviceId;
      DtSent = dtSent;
      Value = value;
      Type = (MeasurementType)measurementType;
    }

    public int Id { get; set; }

    public int DeviceId { get; set; }

    public DateTime DtSent { get; set; }

    public double Value { get; set; }

    public MeasurementType Type { get; set; }

  }
}
