using System;
using System.Collections.Generic;
using System.Linq;
using SmartHub.BL.Converters;
using SmartHub.BusinessLogic.Contracts;
using SmartHub.Infrastructure.Contracts;
using Device = SmartHub.BusinessLogic.Models.Device;
using Measurement = SmartHub.BusinessLogic.Models.Measurement;

namespace SmartHub.BusinessLogic.Services
{
  public class DeviceService : IDeviceService
  {

    private readonly IUnitOfWork _unitOfWork;

    public DeviceService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public void AddDevice(Device device)
    {
      var domainDeviceModel = BlToDomainConverter.ToDomainDeviceModel(device);
       _unitOfWork.Devices.Add(domainDeviceModel);

      _unitOfWork.Commit();
    }

    public void RegisterDevice(string name, string description)
    {

      var device = new Infrastructure.Entities.Device
      {
        Description = description,
        Name = name,
        DeviceId = Guid.NewGuid()
      };

      _unitOfWork.Devices.Add(device);
      _unitOfWork.Commit();
    }

    public void UpdateDevice(int id, Device device)
    {
      var domainDeviceModel = BlToDomainConverter.ToDomainDeviceModel(device);

      var existingDevice = _unitOfWork.Devices.Find(d => d.Id == id).FirstOrDefault();
      if (existingDevice == null)
        return;

      existingDevice.Name = device.Name;

      _unitOfWork.Commit();
    }

    public Device GetDeviceById(int id)
    {
      var device = _unitOfWork.Devices.Find(d => d.Id == id).FirstOrDefault();

      if (device == null) return null;
      var blDevice = DomainToBlConverter.ToBlDevice(device);
      return blDevice;
    }

    public IEnumerable<Measurement> GetTelemetry(int deviceId, DateTime? from, DateTime? end)
    {

      var telemetries = _unitOfWork.Measurements.Find(t => t.Id == deviceId && t.DtSend < end && t.DtSend > end);

      var convertedTelemetries = telemetries.Select(DomainToBlConverter.ToMeasurement);

      return convertedTelemetries;

    }

    public IEnumerable<Device> GetDevices()
    {
      var devices = _unitOfWork.Devices.GetAll();

      return devices.Select(d => new Device
      {
        Id = d.Id,
        Name = d.Name,
        DeviceId = d.DeviceId,
        Description = d.Description
      });
    }



    public void DeleteDevice(int deviceId)
    {
      _unitOfWork.Devices.Remove(new Infrastructure.Entities.Device { Id = deviceId});
      _unitOfWork.Commit();
    }
  }
}
