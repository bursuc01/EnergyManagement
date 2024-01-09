using SiteDevice.DataLayer.DTO;

namespace SiteDevice.BusinessLogicLayer.DeviceBLL;

public interface IDeviceService
{
    Task<IEnumerable<DeviceGetDTO>> GetDevicesAsync();
    Task<DeviceDTO> GetDeviceAsync(int id);
    Task PutDeviceAsync(DeviceGetDTO device);
    Task PostDeviceAsync(DeviceDTO device);
    Task DeleteDeviceAsync(int id);
}