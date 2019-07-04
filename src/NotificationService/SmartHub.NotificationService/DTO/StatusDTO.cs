using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.NotificationService.DTO
{
  public class StatusDTO
  {
    public Guid DeviceId { get; set; }

    public byte Status { get; }

  }
}
