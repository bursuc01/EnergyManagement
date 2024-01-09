using SiteDevice.DataLayer.DTO;
using SiteDevice.DataLayer.Models;

namespace SiteDevice.BusinessLogicLayer.UserBLL;

public interface IUserService
{
    public Task PostUserAsync(UserDTO user);
    public Task DeleteUserAsync(int id);
    public Task LinkDeviceToUserAsync(int deviceId, int userId);
    public Task<ICollection<DeviceDTO>?> GetDevicesOfUserAsync(int id);
    public Task UnlinkDeviceFromUserAsync(int deviceId, int userId);
}