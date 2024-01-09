using SiteMonitor.DataLayer.DTO;

namespace SiteMonitor.BusinessLogicLayer.EnergyConsumptionBLL;

public interface IEnergyConsumptionService
{
    public Task PostMessageAsync(EnergyConsumptionDTO? message);
    public Task DeleteMessagesWithDeviceIdAsync(int deviceId);
    public Task<IEnumerable<EnergyConsumptionDTO>> GetMessagesAsync();
}