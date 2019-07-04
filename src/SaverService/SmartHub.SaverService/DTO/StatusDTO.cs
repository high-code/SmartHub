using System;

namespace SmartHub.SaverService.DTO
{
  public class StatusDto
  {
    public Guid DeviceId { get; set; }

    public byte Status { get; set; }

    public DateTime DtSend { get; set; }
  }
}
