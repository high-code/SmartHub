using System;
using System.Collections.Generic;
using SmartHub.BL.Models;
using SmartHub.BusinessLogic.Models;

namespace SmartHub.BL.Contracts
{
  public interface IDeviceService
  {
    void AddDevice(Device device);

    void RegisterDevice(string name, string description);

    void UpdateDevice(int id,Device device);

    Device GetDeviceById(int id);

    IEnumerable<Device> GetDevices();

    void DeleteDevice(int deviceId);

  }
}
