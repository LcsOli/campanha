using Microsoft.AspNetCore.Mvc;
using Campaign.API.Commands.User.Auth;
using Campaign.API.Handlers.User.Auth;
using Microsoft.AspNetCore.Authorization;
using Campaign.API.DTOs.Request.User.Auth;

namespace Campaign.API.Controllers.User
{
    [ApiController]
    [Route("api/user/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody] AuthRequest request,
                                              [FromServices] IAuthHandler authHandler)
        {
            var cmd = new AuthCommand(request.Document, request.Password);

            return Accepted(await authHandler.Handle(cmd));
        }
    }
}
