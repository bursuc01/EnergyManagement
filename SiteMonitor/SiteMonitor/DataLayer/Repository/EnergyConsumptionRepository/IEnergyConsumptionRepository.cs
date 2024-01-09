using SiteMonitor.DataLayer.Models;

namespace SiteMonitor.DataLayer.Repository.EnergyConsumptionRepository;

public interface IEnergyConsumptionRepository
{
    public Task PostMessageAsync(EnergyConsumption? message);
    public Task DeleteMessagesWithDeviceIdAsync(int deviceId);
    public Task<List<EnergyConsumption?>> GetMessagesAsync();


}