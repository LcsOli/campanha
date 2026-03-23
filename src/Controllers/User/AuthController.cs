using Campaign.API.Commands.User.Auth;
using Campaign.API.DTOs.Request.User.Auth;
using Campaign.API.Handlers.User.Auth;
using Campaign.API.Handlers.User.AuthOrchestrator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Campaign.API.Controllers.User
{
    [ApiController]
    [Route("api/user/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody] AuthRequest request,
                                              [FromServices] IAuthOrchestratorHandler authOrchestratorHandler)
        {
            var cmd = new AuthOrchestratorCommand(request.Document, request.Password);
            return Accepted(await authOrchestratorHandler.Handle(cmd));
        }
    }
}
