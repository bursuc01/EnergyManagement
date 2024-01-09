using System.IdentityModel.Tokens.Jwt;
using SiteUser.DataLayer.Models;

namespace SiteUser.BusinessLogicLayer.TokenBLL;

public interface ITokenService
{ 
        string CreateTokenOptions(User user); 
}