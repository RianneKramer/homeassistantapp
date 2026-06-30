using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dashboard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyController(IEnergyOverviewService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<EnergyOverviewDto>> Get()
        {
            return Ok(await service.GetOverviewAsync());
        }
    }
}
