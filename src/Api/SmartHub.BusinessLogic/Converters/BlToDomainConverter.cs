using System;
using System.Collections.Generic;
using System.Text;
using SmartHub.BusinessLogic.Models;

namespace SmartHub.BL.Converters
{
  public static class BlToDomainConverter
  {
    public static Domain.Entities.Device ToDomainDeviceModel(Device device)
    {
      return new Domain.Entities.Device
      {
        Name = device.Name,
      };
    }
  }
}
