using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiteUser.BusinessLogicLayer.TokenBLL;
using SiteUser.BusinessLogicLayer.UserBLL;
using Site.DataLayer.DTO;
using SiteUser.DataLayer.Models;
using SiteUser.DataLayer.DTO;

namespace SiteUser.PresentationLayer.Controllers;

[Route("api/Authenticate")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _authService;
    private readonly IMapper _mapper;
    
    public AuthenticationController(IUserService userService,
        ITokenService authService,
        IMapper mapper)
    {
        _userService = userService;
        _authService = authService;
        _mapper = mapper;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO? inputUser)
    {
        if (inputUser is null)
        {
            return BadRequest("Invalid client request");
        }

        // Get the list of users
        var userList = await _userService.GetUsersAsync();

        var foundUser = userList.FirstOrDefault(user =>
            user.Name != null &&
            user.Name.Equals(inputUser.Name) &&
            user.Password.Equals(inputUser.Password));

        if (foundUser == null)
        {
            return Unauthorized();
        }

        var user = _mapper.Map<User>(foundUser);
        var tokenString = _authService.CreateTokenOptions(user);

        return Ok(new AuthenticatedResponseDTO { Token = tokenString });
            
    }
}