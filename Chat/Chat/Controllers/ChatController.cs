using Chat.Hub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController : ControllerBase
{
    private IHubContext<ChatHub, IChatClient> _context;

    public ChatController(
        IHubContext<ChatHub, IChatClient> context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<ActionResult> PostMessage(string message)
    {
        await _context.Clients.All.ReceiveMessage(message);

        return NoContent();
    }
    
}