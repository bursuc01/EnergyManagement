using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteMonitor.BusinessLogicLayer.EnergyConsumptionBLL;
using SiteMonitor.DataLayer.DTO;

namespace SiteMonitor.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnergyConsumptionController : ControllerBase
    {
        private readonly IEnergyConsumptionService _energyConsumptionService;

        public EnergyConsumptionController(
            IEnergyConsumptionService energyConsumptionService)
        {
            _energyConsumptionService = energyConsumptionService;
        }
        
        [HttpPost]
        public async Task<ActionResult<EnergyConsumptionDTO>> PostMessageAsync(EnergyConsumptionDTO? message)
        {
            await _energyConsumptionService.PostMessageAsync(message);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<ActionResult<EnergyConsumptionDTO>> DeleteMessagesAsync(int deviceId)
        {
            await _energyConsumptionService.DeleteMessagesWithDeviceIdAsync(deviceId);
            return Ok();
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnergyConsumptionDTO>>> GetMessagesAsync()
        {
            var messages = await _energyConsumptionService.GetMessagesAsync();
            return Ok(messages);
        }
        

    }
}
