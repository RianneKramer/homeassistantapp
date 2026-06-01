using dashboard_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dashboard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAssistant(HomeAssistantRestService restService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            return Ok(await restService.GetApi());
        }

        [HttpGet("states")]
        public async Task<IActionResult> GetStates()
        {
            return Ok(await restService.GetEntities());
        }
    }
}
