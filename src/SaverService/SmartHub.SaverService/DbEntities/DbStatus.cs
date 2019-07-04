using System;

namespace SmartHub.SaverService.DbEntities
{
  public class DbStatus
  {
    public int Id { get; set; }

    public Guid DeviceId { get; set; }

    public byte Status { get; set; }

    public DateTime DtSend { get; set; }
  }
}
