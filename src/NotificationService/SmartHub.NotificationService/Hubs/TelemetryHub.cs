using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SmartHub.NotificationService.Hubs
{
  public class TelemetryHub : Hub
  {

    public TelemetryHub()
    { }

    public override async Task OnConnectedAsync()
    {
      await base.OnConnectedAsync();
    }

    public async Task Subscribe(string groupName)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

  }
}
