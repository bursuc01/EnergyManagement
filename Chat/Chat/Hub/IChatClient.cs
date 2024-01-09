namespace Chat;

public interface IChatClient
{
    Task ReceiveMessage(string message);
    Task UserConnected(string message);
    Task OnlineUsers(string sender, string message);
}