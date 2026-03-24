using Microsoft.AspNetCore.Mvc;
using Campaign.API.Commands.User.Auth;
using Microsoft.AspNetCore.Authorization;
using Campaign.Shared.DTOs.Request.User.Auth;
using Campaign.API.Handlers.User.AuthOrchestrator;

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
