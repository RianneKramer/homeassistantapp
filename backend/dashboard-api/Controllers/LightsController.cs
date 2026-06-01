using dashboard_api.Dtos;
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
        [HttpGet("states")]
        public async Task<IActionResult> GetStates()
        {
            return Ok(await restService.GetApi());
        }
        
        [HttpGet]
        public IActionResult GetLights()
        {
            return Ok(lightStateService.GetLights());
        }

        [HttpPatch("{entityId}")]
        public async Task<IActionResult> UpdateLight([FromRoute]string entityId, [FromQuery]string state, [FromQuery] int? brightness)
        {
            ArgumentNullException.ThrowIfNull(state);

            await restService.LightUpdate(entityId, state, brightness);
            return Ok();
        }
    }
}
