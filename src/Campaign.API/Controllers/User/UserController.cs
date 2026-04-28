using Microsoft.AspNetCore.Mvc;
using Campaign.API.Commands.User.Get;
using Campaign.API.Commands.User.Create;
using Microsoft.AspNetCore.Authorization;
using Campaign.API.Handlers.User.GetUser;
using Campaign.API.DTOs.Request.User.Create;
using Campaign.API.Handlers.User.RegisterUser;

namespace Campaign.API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request,
                                                  [FromServices] IRegisterUserHandler registerUserHandler)
        {
            var cmd = new RegisterUserCommand(request.Role, request.TeamId, request.Name, request.SellerId, request.SupplierId, request.Document, request.Password);
            return Created(string.Empty, await registerUserHandler.Handle(cmd));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id,
                                                 [FromServices] IGetUserHandler getUserHandler)
        {
            var cmd = new GetUserCommand(id);
            return Ok(await getUserHandler.Handle(cmd));
        }
    }
}