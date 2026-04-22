using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.DTO.Page.Request;
using Campaign.API.Handlers.SellerScore.GetSellersScores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Campaign.API.Controllers.SellerScore
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerScoreController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetRanking(IGetSellersScoresHnadler getSellersScoresHnadler)
        {
            return Ok(await getSellersScoresHnadler.Handle());
        }

        [HttpGet("by-team")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetRankingByTeam([FromQuery] int teamId,
                                                          [FromQuery] string? filter,
                                                          [FromQuery] PageRequest page,
                                                          IGetSellersScoresHnadler getSellersScoresHnadler)
        {
            var command = new GetSellersScoresByTeamCommand(teamId, filter!, page.Page, page.Size);
            return Ok(await getSellersScoresHnadler.Handle(command));
        }
    }
}
