using Microsoft.EntityFrameworkCore;
using SiteDevice.DataLayer.Context;
using SiteDevice.DataLayer.Models;

namespace SiteDevice.DataLayer.Repository.DeviceRepository;

public class DeviceRepository : IDeviceRepository
{
    private readonly DataContext _dataContext;

    public DeviceRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<IEnumerable<Device>> GetDevicesAsync()
    {
        return await _dataContext.Devices.ToListAsync();
    }

    public async Task<Device> GetDeviceAsync(int id)
    {
        return await _dataContext.Devices.FirstAsync(device => device.Id == id);
    }

    public async Task PutDeviceAsync(Device device)
    {
        _dataContext.Devices.Update(device);
        await _dataContext.SaveChangesAsync();
    }

    public async Task PostDeviceAsync(Device device)
    {
        device.User = null;
        await _dataContext.Devices.AddAsync(device);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteDeviceAsync(int id)
    {
        var device = await _dataContext
            .Devices
            .Include(device => device.User)
            .FirstAsync(device => device.Id == id);
        
        device.User?.Devices?.Remove(device);
        device.User = null;
        
        _dataContext.Devices.Remove(device);
        await _dataContext.SaveChangesAsync();
    }
}