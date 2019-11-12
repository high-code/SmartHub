using System;
using System.Collections.Generic;
using Device = SmartHub.BusinessLogic.Models.Device;
using Measurement = SmartHub.BusinessLogic.Models.Measurement;

namespace SmartHub.BusinessLogic.Contracts
{
  public interface IDeviceService
  {
    void AddDevice(Device device);

    void RegisterDevice(string name, string description, Guid userId);

    void UpdateDevice(int id,Device device);

    Device GetDeviceById(int id);

    IEnumerable<Measurement> GetTelemetry(int deviceId, DateTime? from, DateTime? end);

    IEnumerable<Device> GetDevices(Guid userId);

    void DeleteDevice(int deviceId);

  }
}
