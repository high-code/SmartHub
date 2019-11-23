using System;
using SmartHub.BusinessLogic.Models;
using SmartHub.Infrastructure.Entities;
using Device = SmartHub.Infrastructure.Entities.Device;

namespace SmartHub.BL.Converters
{
  public static class DomainToBlConverter
  {

    public static BusinessLogic.Models.Device ToBlDevice(Device device)
    {
      return new BusinessLogic.Models.Device
      {
        Name = device.Name,
        Id = device.Id,
        Description = device.Description,
        DeviceId = device.DeviceId
      };
    }
  }
}
