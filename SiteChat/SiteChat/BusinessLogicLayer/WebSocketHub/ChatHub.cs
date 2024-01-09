using Microsoft.AspNetCore.SignalR;
using SiteChat.BusinessLogicLayer.MessageService;

namespace SiteChat;

public sealed class ChatHub : Hub
{
    private readonly MessageService _messageService;
    
    public ChatHub(MessageService messageService)
    {
        _messageService = messageService;
    }
    
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "AdminChat");
        await Clients.Caller.SendAsync("UserConnected","Connected!");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "AdminChat");
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string text)
    {
        Console.Write(text);
        await Clients.All.SendAsync("ReceiveMessage",$"{Context.ConnectionId}: {text}");
    }

    public async Task AddUserConnectionId(string name)
    {
        _messageService.AddUserConnectionId(name, Context.ConnectionId);
        var onlineUsers = _messageService.GetOnlineUsers();
        await Clients.Groups("AdminChat").SendAsync("OnlineUsers", onlineUsers);
    }
}