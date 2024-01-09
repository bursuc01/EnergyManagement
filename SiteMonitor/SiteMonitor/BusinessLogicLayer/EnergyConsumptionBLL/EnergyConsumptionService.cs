using AutoMapper;
using SiteMonitor.DataLayer.DTO;
using SiteMonitor.DataLayer.Models;
using SiteMonitor.DataLayer.Repository.EnergyConsumptionRepository;

namespace SiteMonitor.BusinessLogicLayer.EnergyConsumptionBLL;

public class EnergyConsumptionService : IEnergyConsumptionService
{
    private readonly IEnergyConsumptionRepository _energyConsumptionRepository;
    private readonly IMapper _mapper;
    
    public EnergyConsumptionService(
        IEnergyConsumptionRepository energyConsumptionRepository,
        IMapper mapper)
    {
        _energyConsumptionRepository = energyConsumptionRepository;
        _mapper = mapper;
    }
    
    public Task PostMessageAsync(EnergyConsumptionDTO? message)
    {
        var realMessage = _mapper.Map<EnergyConsumption>(message);
        return _energyConsumptionRepository.PostMessageAsync(realMessage);
    }

    public async Task DeleteMessagesWithDeviceIdAsync(int deviceId)
    {
        await _energyConsumptionRepository.DeleteMessagesWithDeviceIdAsync(deviceId);
    }

    public async Task<IEnumerable<EnergyConsumptionDTO>> GetMessagesAsync()
    {
        var messages = await _energyConsumptionRepository.GetMessagesAsync();
        return _mapper.Map<IEnumerable<EnergyConsumptionDTO>>(messages);
    }
}