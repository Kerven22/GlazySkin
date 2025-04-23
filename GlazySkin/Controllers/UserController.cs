using Microsoft.AspNetCore.Mvc;

namespace GlazySkin.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(); 
        }
    }
}
