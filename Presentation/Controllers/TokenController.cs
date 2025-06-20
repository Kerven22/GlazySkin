using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("token")]
    public class TokenController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _serviceManager.AuthenticationService.RefreshToken(tokenDto);

            return Ok(tokenDtoToReturn);
        }
    }
}
