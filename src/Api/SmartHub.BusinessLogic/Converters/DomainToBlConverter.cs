using System;
using SmartHub.BusinessLogic.Models;
using SmartHub.Domain.Entities;
using Device = SmartHub.Domain.Entities.Device;
using Measurement = SmartHub.Domain.Entities.Measurement;

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

    public static BusinessLogic.Models.Measurement ToMeasurement(Measurement measurement)
    {
      if (measurement == null) return null;

      return new BusinessLogic.Models.Measurement
      {
        DtSent = measurement.DtSend.Value,
        Type = (MeasurementType) measurement.Type,
        Value = measurement.Value
      };
    }
  }
}
