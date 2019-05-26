using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SmartHub.NotificationService.Hubs;

namespace SmartHub.NotificationService
{

  public interface INotificationService
  {
    void Notify(Telemetry telemetry);
  }

  public class NotificationService : INotificationService
  {
    private readonly IHubContext<TelemetryHub> _telemetryHub;

    public NotificationService(IHubContext<TelemetryHub> hub)
    {
      _telemetryHub = hub;
    }

    public void Notify(Telemetry telemetry)
    {
      _telemetryHub.Clients.Group(telemetry.DeviceId.ToString()).SendAsync("notify", telemetry);
    }
  }
}
