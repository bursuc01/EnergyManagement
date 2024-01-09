using Microsoft.AspNetCore.SignalR;

namespace SiteMonitor.BusinessLogicLayer.WebSocketService;
public sealed class SocketHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", "Socket started with success");
    }
}