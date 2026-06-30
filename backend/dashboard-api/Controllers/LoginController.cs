using dashboard_api.Dtos;
using dashboard_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dashboard_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginService service) : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDto user)
        {
            var token = service.LoginAsync(user.Username, user.Password);
            Console.WriteLine(token);
            return Ok(new {token.Result});
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult("Authenticated");
        }
    }
}
