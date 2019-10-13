using System;
using System.Collections.Generic;
using System.Text;
using SmartHub.BusinessLogic.Models;

namespace SmartHub.BL.Converters
{
  public static class BlToDomainConverter
  {
    public static Infrastructure.Entities.Device ToDomainDeviceModel(Device device)
    {
      return new Infrastructure.Entities.Device
      {
        Id = device.Id,
        Name = device.Name,
        DeviceId = device.DeviceId,
        Description = device.Description,
        UserId = device.UserId
      };
    }
  }
}
