using dashboard_api.Models;
using dashboard_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dashboard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightsController(LightStateService lightStateService, HomeAssistantRestService restService)
        : ControllerBase
    {
        [HttpGet]
        public IActionResult GetLights()
        {
            return Ok(lightStateService.GetLights());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateLight(int id, [FromBody] LightEntity light)
        {
            await lightStateService.UpdateLight(light);

            await restService.ToggleLight(light.EntityId, light.IsOn);
            
            await restService.SetBrightness(light.EntityId, light.Brightness);
            return Ok();
        }
    }
}
