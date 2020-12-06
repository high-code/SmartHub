using System;

namespace SmartHub.Edge.Application.DTO
{

  public class MeasurementDTO
  {

    public MeasurementDTO(Guid deviceId, DateTime sent,
                          int measurementType, double value,
                          DateTime dtReceived)
    {
      DeviceId = deviceId;
      DtSent = sent;
      MeasurementType = measurementType;
      Value = value;

    }
    public Guid DeviceId { get; set; }

    public DateTime DtSent { get; set; }

    public DateTime DtReceived { get; set; }

    public int MeasurementType { get; set; }

    public double Value { get; set; }

  }
}
