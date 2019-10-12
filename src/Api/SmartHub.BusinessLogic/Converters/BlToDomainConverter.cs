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
        Name = device.Name,
      };
    }
  }
}
