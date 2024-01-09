using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SiteUser.DataLayer.Models;

namespace SiteUser.BusinessLogicLayer.TokenBLL;

public class TokenService : ITokenService
{
    private IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateTokenOptions(User user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.Name),
            new Claim("IsAdmin", user.IsAdmin.ToString()),
        };
            
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: "https://localhost:7006",
            audience: "https://localhost:7006",
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: signinCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    }
    
}