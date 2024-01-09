using SiteDevice.DataLayer.Models;

namespace SiteDevice.DataLayer.Repository.DeviceRepository;

public interface IDeviceRepository
{
    public Task<IEnumerable<Device>> GetDevicesAsync();
    public Task<Device> GetDeviceAsync(int id);
    public Task PutDeviceAsync(Device device);
    public Task PostDeviceAsync(Device device);
    public Task DeleteDeviceAsync(int id);
}