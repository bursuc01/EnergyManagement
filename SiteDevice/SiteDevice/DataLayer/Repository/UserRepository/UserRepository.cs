using Microsoft.EntityFrameworkCore;
using SiteDevice.DataLayer.Context;
using SiteDevice.DataLayer.Models;

namespace SiteDevice.DataLayer.Repository.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task PostUserAsync(User user)
    {
        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _dataContext
            .Users
            .Include(user => user.Devices)
            .FirstAsync(user => user.Id == id);

        _dataContext.Users.Remove(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task LinkDeviceToUserAsync(int deviceId, int userId)
    {
        var user = await _dataContext
            .Users
            .Include(user => user.Devices)
            .FirstAsync(user => user.Id == userId);
        var device = await _dataContext
            .Devices
            .Include(device => device.User)
            .FirstAsync(device => device.Id == deviceId);
        
        user.Devices?.Add(device);
        device.User = user;
        await _dataContext.SaveChangesAsync();
    }

    public async Task<ICollection<Device>?> GetDevicesOfUserAsync(int id)
    {
        var user = await _dataContext
            .Users
            .Include(user => user.Devices)
            .FirstAsync(user => user.Id == id);

        return user.Devices;
    }

    public async Task UnlinkDeviceFromUserAsync(int deviceId, int userId)
    {
        var user = await _dataContext
            .Users
            .Include(user => user.Devices)
            .FirstAsync(user => user.Id == userId);
        var device = await _dataContext
            .Devices
            .Include(device => device.User)
            .FirstAsync(device => device.Id == deviceId);

        user.Devices?.Remove(device);
        device.User = null;
        await _dataContext.SaveChangesAsync();
    }
}