using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.NotificationService.DTO
{
  public class MeasurementDTO
  {
    public Guid DeviceId { get; set; }

    public byte Type { get; set; }

    public double Value { get; set; }
  }
}
