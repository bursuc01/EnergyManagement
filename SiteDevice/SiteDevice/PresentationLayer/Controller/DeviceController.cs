using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteDevice.BusinessLogicLayer.DeviceBLL;
using SiteDevice.DataLayer.DTO;

namespace SiteDevice.PresentationLayer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        // GET: api/Device
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceGetDTO>>> GetDevices()
        {
            var devices = await _deviceService.GetDevicesAsync();

            return Ok(devices);
        }

        // GET: api/Device/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDTO>> GetDevice(int id)
        {
            var device = await _deviceService.GetDeviceAsync(id);

            return Ok(device);
        }

        // PUT: api/Device/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutDevice(DeviceGetDTO device)
        {
            await _deviceService.PutDeviceAsync(device);
            return Ok();
        }

        // POST: api/Device
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeviceDTO>> PostDevice(DeviceDTO device)
        {
            await _deviceService.PostDeviceAsync(device);
            return Ok();
        }

        // DELETE: api/Device/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            await _deviceService.DeleteDeviceAsync(id);
            return Ok();
        }
    }
}
