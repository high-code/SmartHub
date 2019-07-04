using System;

namespace SmartHub.SaverService.DbEntities
{

  public class DbMeasurement
  {
    public int Id { get; set; }

    public byte Type { get; set; }

    public double Value { get; set; }

    public Guid DeviceId { get; set; }

    public DateTime DtSend { get; set; }
  }
}
