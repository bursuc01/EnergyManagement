using SiteDevice.DataLayer.Models;

namespace SiteDevice.DataLayer.Repository.UserRepository;

public interface IUserRepository
{
    public Task PostUserAsync(User user);
    public Task DeleteUserAsync(int id);
    public Task LinkDeviceToUserAsync(int deviceId, int userId);
    public Task<ICollection<Device>?> GetDevicesOfUserAsync(int id);
    public Task UnlinkDeviceFromUserAsync(int deviceId, int userId);


}