using Microsoft.EntityFrameworkCore;
using SiteUser.DataLayer.Context;
using SiteUser.DataLayer.Models;

namespace SiteUser.DataLayer.Repository.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;
    
    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _dataContext.Users.ToListAsync();
    }
    
    public async Task<User> GetUserAsync(int id)
    {
        return await _dataContext.Users.FirstAsync(user => user.Id == id);
    }
    
    public async Task<User> GetUserAsync(string name)
    {
        return await _dataContext.Users.FirstAsync(user => user.Name != null && user.Name.Equals(name));
    }

    public async Task PutUserAsync(User user)
    {
        _dataContext.Users.Update(user);
        await _dataContext.SaveChangesAsync();

    }

    public async Task PostUserAsync(User user)
    {
        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _dataContext.Users.FirstAsync(user => user.Id == id);
        _dataContext.Users.Remove(user);
        await _dataContext.SaveChangesAsync();
    }
}