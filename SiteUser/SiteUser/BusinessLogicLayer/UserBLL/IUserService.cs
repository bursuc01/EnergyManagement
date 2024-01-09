using Site.Migrations;
using SiteUser.DataLayer.DTO;

namespace SiteUser.BusinessLogicLayer.UserBLL;

public interface IUserService
{
    public Task<IEnumerable<UserDTO>> GetUsersAsync();
    public Task<UserDTO> GetUserAsync(int id);
    public Task PutUserAsync(UserDTO user);
    public Task PostUserAsync(UserDTO user);
    public Task DeleteUserAsync(int id);
}