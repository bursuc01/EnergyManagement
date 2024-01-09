using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteDevice.BusinessLogicLayer.UserBLL;
using SiteDevice.DataLayer.DTO;

namespace SiteDevice.PresentationLayer.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/Device
    [HttpPost]
    public async Task<ActionResult> PostUserAsync(UserDTO user)
    {
        await _userService.PostUserAsync(user);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        await _userService.DeleteUserAsync(id);

        return Ok();
    }

    [HttpPut("link")]    
    [Authorize]
    public async Task<IActionResult> LinkDeviceToUserAsync(int deviceId, int userId)
    {
        await _userService.LinkDeviceToUserAsync(deviceId, userId);
        return Ok();
    }
    
    [HttpPut("unlink")]  
    [Authorize]
    public async Task<IActionResult> UnlinkDeviceFromUserAsync(int deviceId, int userId)
    {
        await _userService.UnlinkDeviceFromUserAsync(deviceId, userId);
        return Ok();
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ICollection<DeviceDTO>>> GetDevicesOfUserAsync(int id)
    {
        var devices = await _userService.GetDevicesOfUserAsync(id);
        return Ok(devices);
    }
}
