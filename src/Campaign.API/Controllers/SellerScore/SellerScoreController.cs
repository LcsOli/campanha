using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.Handlers.SellerScore.GetSellersScores;

namespace Campaign.API.Controllers.SellerScore
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerScoreController : ControllerBase
    {
        [HttpGet("by-filter")]
        [Authorize(Roles = "manager, user")]
        public async Task<IActionResult> GetRankingByTeam([FromQuery] int? teamId,
                                                          [FromQuery] string? filter,
                                                          [FromQuery] short? page,
                                                          [FromQuery] short? size,
                                                          IGetSellersScoresHandler getSellersScoresHnadler)
        {
            var command = new GetSellersScoresByFiltersCommand(teamId, filter!, page, size);
            return Ok(await getSellersScoresHnadler.Handle(command));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "manager, user")]
        public async Task<IActionResult> Get(int id ,IGetSellersScoresHandler getSellersScoresHnadler)
        {
            var command = new GetSellerScoreByIdCommand(id);
            return Ok(await getSellersScoresHnadler.Handle(command));
        }
    }
}
