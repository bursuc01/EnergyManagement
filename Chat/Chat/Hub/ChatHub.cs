using Chat.DataLayer.ChatLogs;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hub;

public sealed class ChatHub : Hub <IChatClient>
{
    private static readonly Dictionary<string, string> Users = new Dictionary<string, string>();

    public override async Task OnConnectedAsync()
    {
        await Clients.All.UserConnected($"{Context.ConnectionId} has joined!");
    }

    public async Task AddUserToLogs(string username)
    {
        if (!Users.ContainsKey(username))
        {
            Users.Add(username, Context.ConnectionId);
        }
        await Clients.All.UserConnected($"{Context.ConnectionId}: {username} is now online!");
    }
    
    public async Task SendMessage(string sender, string receiver, string message)
    {
        Console.WriteLine($"Received message from {sender} to {receiver}: {message}");
        if (Users.ContainsKey(receiver))
        {
            await Clients.All.ReceiveMessage($"{sender}: {message}");    
            Console.WriteLine($"Sent message to {receiver}");
        }
    }
}