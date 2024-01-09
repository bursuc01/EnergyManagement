using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteUser.BusinessLogicLayer.UserBLL;
using SiteUser.DataLayer.DTO;

namespace SiteUser.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserFrontendDTO>>> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserFrontendDTO>> GetUserAsync(int id)
        {
            var user = await _userService.GetUserAsync(id);
            return Ok(user);
        }

        // PUT: api/User
        [HttpPut]
        public async Task<IActionResult> PutUserAsync(UserDTO user)
        {
            await _userService.PutUserAsync(user);
            return Ok();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUserAsync(UserDTO user)
        {
            await _userService.PostUserAsync(user);
            return Ok();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }
    }
        
}
