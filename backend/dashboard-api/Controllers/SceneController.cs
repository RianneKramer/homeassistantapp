using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dashboard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SceneController(ISceneManagementService sceneManagementService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<SceneResponseDto>>> GetAll()
        {
            return Ok(await sceneManagementService.GetScenes());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SceneResponseDto>> Get(int id)
        {
            var scene = await sceneManagementService.GetScene(id);

            return scene == null
                ? NotFound()
                : Ok(scene);
        }

        [HttpPost]
        public async Task<ActionResult<SceneResponseDto>> Create(
            [FromBody] CreateSceneDto dto)
        {
            var scene = await sceneManagementService.CreateSceneAsync(dto);

            return CreatedAtAction(
                nameof(Get),
                new { id = scene.Id },
                scene);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SceneResponseDto>> Update(
            int id,
            [FromBody] UpdateSceneDto dto)
        {
            var scene = await sceneManagementService.UpdateSceneAsync(id, dto);

            return scene == null
                ? NotFound()
                : Ok(scene);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await sceneManagementService.DeleteSceneAsync(id);

            return deleted
                ? NoContent()
                : NotFound();
        }

        [HttpPost("{id:int}/enable")]
        public async Task<IActionResult> Enable(int id)
        {
            return await sceneManagementService.EnableSceneAsync(id)
                ? Ok()
                : NotFound();
        }

        [HttpPost("{id:int}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            return await sceneManagementService.DisableSceneAsync(id)
                ? Ok()
                : NotFound();
        }
    }
}
