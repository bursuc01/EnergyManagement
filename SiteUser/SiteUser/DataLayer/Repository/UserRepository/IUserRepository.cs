using SiteUser.DataLayer.Models;

namespace SiteUser.DataLayer.Repository.UserRepository;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsersAsync();
    public Task<User> GetUserAsync(int id);
    public Task<User> GetUserAsync(string name);
    public Task PutUserAsync(User user);
    public Task PostUserAsync(User user);
    public Task DeleteUserAsync(int id);
}