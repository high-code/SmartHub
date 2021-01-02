using System;
using System.Collections.Generic;
using Device = SmartHub.BusinessLogic.Models.Device;

namespace SmartHub.BusinessLogic.Contracts
{
  public interface IDeviceService
  {
    void AddDevice(Device device);

    Infrastructure.Entities.Device RegisterDevice(string name, string description, Guid userId);

    void UpdateDevice(int id,Device device);

    Device GetDeviceById(int id);

    IEnumerable<Device> GetDevices(Guid userId);

    void DeleteDevice(int deviceId);

  }
}
