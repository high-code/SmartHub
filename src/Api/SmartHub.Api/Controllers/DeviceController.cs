using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartHub.BL.Contracts;
using SmartHub.BL.Models;
using SmartHub.BusinessLogic.Models;
using SmartHub.Models;

namespace SmartHub.Controllers
{
  //[Authorize]
  [Route("api/[controller]")]
  public class DeviceController : Controller
  {
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
      _deviceService = deviceService;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var devices = _deviceService.GetDevices().Select(d => new DeviceModel
      {
        Id = d.Id,
        Name = d.Name,
        Description = d.Description
      });

      return Ok(devices);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var device = _deviceService.GetDeviceById(id);

      if (device == null)
        return NotFound();

      var deviceModel = new DeviceModel
      {
        Id = device.Id,
        Name = device.Name,
        Description = device.Description,
        DeviceId = device.DeviceId
      };

      return Ok(deviceModel);
    }

    [HttpGet("{id}/telemetry")]
    public IActionResult GetTelemetry(int id, DateTime? startFrom, DateTime? endTo)
    {


      return Ok();
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody]RegisterDeviceModel registerDeviceModel)
    {
      _deviceService.RegisterDevice(registerDeviceModel.Name, registerDeviceModel.Description);

      return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]DeviceModel device)
    {

      var blDevice = new Device
      {
        Id = device.Id,
        Name = device.Name
      };

      _deviceService.UpdateDevice(id, blDevice);

      return Ok();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      _deviceService.DeleteDevice(id);

      return Ok();
    }
  }
}
