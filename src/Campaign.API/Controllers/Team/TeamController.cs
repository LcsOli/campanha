using Campaign.API.Handlers.Team.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Campaign.API.Controllers.Team
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(IGetTeamHandle getTeamHandle)
        {
            return Ok(await getTeamHandle.Handle());
        }
    }
}
