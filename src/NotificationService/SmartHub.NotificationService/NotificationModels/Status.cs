using System;
using System.Collections.Generic;

namespace SmartHub.NotificationService.NotificationModels
{
  public class Status
  {
    public byte DeviceStatus { get; set; }

    public Guid DeviceId { get; set; }
  }
}
