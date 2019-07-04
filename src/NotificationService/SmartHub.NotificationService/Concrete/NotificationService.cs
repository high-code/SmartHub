using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SmartHub.NotificationService.Hubs;
using SmartHub.NotificationService.NotificationModels;

namespace SmartHub.NotificationService.Concrete
{

  public interface INotificationService
  {
    void SendMeasurementNotification(string rawTelemetry);

    void SendStatusNotification(string rawStatus);

  }

  public class NotificationService : INotificationService
  {
    private readonly IServiceProvider _serviceProvider;

    public NotificationService(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
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

      using (var scope = _serviceProvider.CreateScope())
      {
        var telemetryHub = scope.ServiceProvider.GetRequiredService<IHubContext<TelemetryHub>>();
        var groupName = measurementNotification.DeviceId.ToString();
        telemetryHub.Clients.Group(groupName).SendAsync("sendMeasurement", measurementNotification);
      }

    }


    public void SendStatusNotification(string rawStatus)
    {

      var statusDto = JsonConvert.DeserializeObject<Status>(rawStatus);

      var statusNotification = new Status
      {
        DeviceId = statusDto.DeviceId,
        DeviceStatus = statusDto.DeviceStatus
      };

      using (var scope = _serviceProvider.CreateScope())
      {
        var telemetryHub = scope.ServiceProvider.GetRequiredService<IHubContext<TelemetryHub>>();
        var groupName = statusNotification.DeviceId.ToString();
        telemetryHub.Clients.Group(groupName).SendAsync("sendMeasurement", statusNotification);
      }
    }
  }
}
