using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHub.BusinessLogic.Contracts;
using SmartHub.BusinessLogic.Models;
using SmartHub.Models;

namespace SmartHub.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class DeviceController : Controller
  {
    private readonly IDeviceService _deviceService;
    private readonly ILogger<DeviceController> _logger;

    public DeviceController(IDeviceService deviceService, ILogger<DeviceController> logger)
    {
      _logger = logger;
      _deviceService = deviceService;
    }

    [HttpGet]
    public IActionResult Get()
    {
      _logger.LogInformation("Request all devices");
      var userId = Guid.Parse(User.Identity.Name);
      var devices = _deviceService.GetDevices(userId).Select(d => new DeviceModel
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
      _logger.LogInformation("Requesting device with id = {Id}", id);
      var device = _deviceService.GetDeviceById(id);

      if (device == null)
      {
        _logger.LogWarning("Device with id={id} not found", id);
        return NotFound();
      }

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
      var telemetries = _deviceService.GetTelemetry(id, startFrom, endTo);

      return Ok(telemetries);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody]RegisterDeviceModel registerDeviceModel)
    {
      _logger.LogInformation("Registering new device", registerDeviceModel);
      var userId = Guid.Parse(User.Identity.Name);
      _deviceService.RegisterDevice(registerDeviceModel.Name, registerDeviceModel.Description, userId);

      return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]DeviceModel device)
    {
      _logger.LogInformation("Updating device with id={id}", device.Id);
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
      _logger.LogInformation("Deleting device with id={id}", id);
      _deviceService.DeleteDevice(id);

      return Ok();
    }
  }
}
