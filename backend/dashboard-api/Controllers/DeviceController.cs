using dashboard_api.Dtos;
using dashboard_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dashboard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController(DeviceControllerService deviceController) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Execute([FromBody] DeviceCommandDto command)
        {
            await deviceController.ExecuteAsync(command);
            
            return Ok();
        }
    }
}
