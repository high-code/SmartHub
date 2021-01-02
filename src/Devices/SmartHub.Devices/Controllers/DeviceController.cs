using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHub.BusinessLogic.Contracts;
using SmartHub.BusinessLogic.Models;
using SmartHub.Devices.Application.Commands;
using SmartHub.Models;

namespace SmartHub.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class DeviceController : Controller
  {
    private readonly IDeviceService _deviceService;
    private readonly IMediator _mediator;

    private readonly ILogger<DeviceController> _logger;

    public DeviceController(IDeviceService deviceService, IMediator mediator, ILogger<DeviceController> logger)
    {
      _logger = logger;
      _deviceService = deviceService;
      _mediator = mediator;
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


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterDeviceCommand registerDeviceCommand)
    {
      _logger.LogInformation("Registering new device", registerDeviceCommand);

      var success = await _mediator.Send(registerDeviceCommand);

      if (!success) return BadRequest();

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
