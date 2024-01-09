using Microsoft.EntityFrameworkCore;
using SiteMonitor.DataLayer.Context;
using SiteMonitor.DataLayer.Models;

namespace SiteMonitor.DataLayer.Repository.EnergyConsumptionRepository;

public class EnergyConsumptionRepository : IEnergyConsumptionRepository
{
    private readonly DataContext _dataContext;

    public EnergyConsumptionRepository(
        DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task PostMessageAsync(EnergyConsumption? message)
    {
        await _dataContext.EnergyConsumptions.AddAsync(message);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteMessagesWithDeviceIdAsync(int deviceId)
    {
        var messageList = await _dataContext.EnergyConsumptions
            .Where(elem => elem.DeviceId == deviceId)
            .ToListAsync();

        foreach (var message in messageList)
        {
            _dataContext.EnergyConsumptions.Remove(message);
        }

        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<EnergyConsumption?>> GetMessagesAsync()
    {
        var messages = await _dataContext.EnergyConsumptions.ToListAsync();
        return messages;
    }
}