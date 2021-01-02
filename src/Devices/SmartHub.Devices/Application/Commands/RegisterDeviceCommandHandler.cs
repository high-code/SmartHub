using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartHub.BusinessLogic.Contracts;
using SmartHub.Devices.Application.IntegrationEvents;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Devices.Application.Commands
{
  public class RegisterDeviceCommandHandler : IRequestHandler<RegisterDeviceCommand, bool>
  {
    private readonly IDeviceService _deviceService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDeviceIntegrationEventLogService _deviceIntegrationEventLogService;
    private readonly ILogger<RegisterDeviceCommandHandler> _logger;

    public RegisterDeviceCommandHandler(IDeviceService deviceService, IHttpContextAccessor httpContextAccessor,
     IDeviceIntegrationEventLogService deviceIntegrationEventLogService, ILogger<RegisterDeviceCommandHandler> logger)
    {
      _deviceIntegrationEventLogService = deviceIntegrationEventLogService;
      _logger = logger;
      _deviceService = deviceService;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> Handle(RegisterDeviceCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Starting {command} handling", request);
      var userId = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
      if (userId == null) return false;
      var parsedUserId = Guid.Parse(userId);
      var device = _deviceService.RegisterDevice(request.Name, request.Description, parsedUserId);

      var deviceRegisteredIntegrationEvent = new DeviceRegisteredIntegrationEvent(device.DeviceId, parsedUserId, device.Name, device.Description);

      await _deviceIntegrationEventLogService.AddEventsAsync(deviceRegisteredIntegrationEvent);

      return true;
    }
  }
}
