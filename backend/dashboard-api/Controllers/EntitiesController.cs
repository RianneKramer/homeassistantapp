using dashboard_api.Dtos;
using dashboard_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dashboard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController(EntitySyncService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<EntityDto>>> GetEntities()
        {
            return Ok(await service.GetEntities());
        }
    }
}
