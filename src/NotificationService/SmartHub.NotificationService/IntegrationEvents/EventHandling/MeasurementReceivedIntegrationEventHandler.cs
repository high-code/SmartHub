using Microsoft.Extensions.Logging;
using SmartHub.Messaging.Abstractions;
using SmartHub.NotificationService.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace SmartHub.NotificationService.IntegrationEvents.EventHandling
{
  public class MeasurementReceivedIntegrationEventHandler : IIntegrationEventHandler<MeasurementReceivedIntegrationEvent>
  {
    private readonly ILogger<MeasurementReceivedIntegrationEventHandler> _logger;
    public MeasurementReceivedIntegrationEventHandler(ILogger<MeasurementReceivedIntegrationEventHandler> logger)
    {
      _logger = logger;
    }

    public async Task Handle(MeasurementReceivedIntegrationEvent integrationEvent)
    {
      _logger.LogInformation("Received integration event", integrationEvent);

      await Task.CompletedTask;
    }
  }
}
