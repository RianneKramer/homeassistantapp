using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dashboard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController(IDeviceControllerService deviceController) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Execute([FromBody] DeviceCommandDto command)
        {
            await deviceController.ExecuteAsync(command);
            
            return Ok();
        }
    }
}
