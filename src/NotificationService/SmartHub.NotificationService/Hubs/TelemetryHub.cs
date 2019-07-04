using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace SmartHub.NotificationService.Hubs
{
  public class TelemetryHub : Hub
  {

    private readonly ILogger<TelemetryHub> _logger;

    public TelemetryHub(ILogger<TelemetryHub> logger)
    {
      _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
      await base.OnConnectedAsync();
    }

    public async Task Subscribe(string groupName)
    {
      // Add check for device existence
      _logger.LogInformation($"{Context.ConnectionId} added succesfully to {groupName}");
      await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

    }

  }
}
