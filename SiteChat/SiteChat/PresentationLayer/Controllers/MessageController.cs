using Microsoft.AspNetCore.Mvc;
using SiteChat.BusinessLogicLayer.MessageService;
using SiteChat.DataLayer.Models;

namespace SiteChat.PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly MessageService _messages;

    public MessageController(
        MessageService messages)
    {
        _messages = messages;
    }

    [HttpPost]
    public IActionResult RegisterUser(string name)
    {
        if (_messages.AddUserToList(name))
        {
            return Ok();
        }
        
        return BadRequest();
    }
}