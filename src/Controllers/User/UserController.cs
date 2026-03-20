using Campaign.API.Commands.User.Create;
using Campaign.API.DTOs.User.Create;
using Campaign.API.Handlers.User.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace Campaign.API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest dto,
                                                  [FromServices] IRegisterUserHandler registerUserHandler)
        {
            var cmd = new RegisterUserCommand(dto.Role, dto.TeamId, dto.Name, dto.ManagerId, dto.Document, dto.Password);
            Ok(await registerUserHandler.Handle());
        }
    }
}
