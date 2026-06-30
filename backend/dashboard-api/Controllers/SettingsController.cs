using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dashboard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController(ISettingsService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<SettingsDto>> Get()
        {
            return Ok(await service.GetAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Update(SettingsDto dto)
        {
            await service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test(SettingsDto dto)
        {
            var result = await service.TestConnectionAsync(dto);

            if (result)
            {
                return Ok(true);
            }
            
            return BadRequest();
        }
    }
}
