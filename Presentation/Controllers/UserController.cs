using GlazySkin.ActionFilter;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttrebute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto user)
        {
            var result = await _serviceManager.AuthenticationService.RegisterUser(user); 

            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Code, error.Description);

                return BadRequest(ModelState);
            }

            return StatusCode(201); 
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttrebute))]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto user)
        {
            if (!await _serviceManager.AuthenticationService.ValidateUser(user))
                return Unauthorized();

            var tokenDto = await _serviceManager.AuthenticationService.CreateToken(populateExp: true); 
            return Ok(tokenDto); 
        }
    }
}
