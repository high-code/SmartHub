using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SmartHub.NotificationService.Hubs;
using SmartHub.NotificationService.NotificationModels;

namespace SmartHub.NotificationService
{

  public interface INotificationService
  {
    void SendMeasurementNotification(string rawTelemetry);

    void SendStatusNotification(string rawStatus);

  }

  public class NotificationService : INotificationService
  {
    private readonly IHubContext<TelemetryHub> _telemetryHub;

    public NotificationService(IHubContext<TelemetryHub> hub)
    {
      _telemetryHub = hub;
    }

    public void SendMeasurementNotification(string rawTelemetry)
    {
      
      var measurementDto = JsonConvert.DeserializeObject<Measurement>(rawTelemetry);

      var measurementNotification = new Measurement()
      {
        DeviceId = measurementDto.DeviceId,
        Type = measurementDto.Type,
        Value = measurementDto.Value
      };
      // check if device exist
      var groupName = measurementNotification.DeviceId.ToString();
      _telemetryHub.Clients.Group(groupName).SendAsync("sendMeasurement", measurementNotification);
    }


    public void SendStatusNotification(string rawStatus)
    {

      var statusDto = JsonConvert.DeserializeObject<Status>(rawStatus);

      var statusNotification = new Status
      {
        DeviceId = statusDto.DeviceId,
        DeviceStatus = statusDto.DeviceStatus
      };

      // check if device exist
      var groupName = statusNotification.DeviceId.ToString();
      _telemetryHub.Clients.Groups(groupName).SendAsync("sendStatus", statusNotification);

    }
  }
}
