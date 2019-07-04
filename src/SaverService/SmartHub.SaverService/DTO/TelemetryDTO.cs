using System;

namespace SmartHub.SaverService.DTO
{

  public class MeasurementDto
  {
    public Guid DeviceId { get; set; }

    public byte Type { get; set; }

    public double Value { get; set; }

    public DateTime DtSend { get; set; }
  }
}
