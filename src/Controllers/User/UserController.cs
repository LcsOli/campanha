using Microsoft.AspNetCore.Mvc;
using Campaign.API.Commands.User.Create;
using Microsoft.AspNetCore.Authorization;
using Campaign.API.Handlers.User.RegisterUser;
using Campaign.API.DTOs.Request.User.Create;

namespace Campaign.API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest dto,
                                                  [FromServices] IRegisterUserHandler registerUserHandler)
        {
            var cmd = new RegisterUserCommand(dto.Role, dto.TeamId, dto.Name, dto.ManagerId, dto.Document, dto.Password);
            return Created(string.Empty, await registerUserHandler.Handle(cmd));
        }
    }
}
