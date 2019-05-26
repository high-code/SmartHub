using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHub.BL.Contracts;
using SmartHub.BL.Converters;
using SmartHub.BL.Helpers;
using SmartHub.BL.Models;
using SmartHub.BusinessLogic.Models;
using SmartHub.Domain.Contracts;

namespace SmartHub.BL.Services
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

      var device = new Domain.Entities.Device
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
      _unitOfWork.Devices.Remove(new Domain.Entities.Device { Id = deviceId});
      _unitOfWork.Commit();
    }
  }
}
