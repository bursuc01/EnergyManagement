namespace SiteChat.DataLayer.Models;

public class Message
{
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }

    public Message(
        string receiver, 
        string sender, 
        string content)
    {
        Receiver = receiver;
        Sender = sender;
        Content = content;
        Timestamp = DateTime.Now;
    }
}