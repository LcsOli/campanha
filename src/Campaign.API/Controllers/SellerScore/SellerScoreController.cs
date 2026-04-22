using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Campaign.API.Handlers.SellerScore.GetSellersScores;

namespace Campaign.API.Controllers.SellerScore
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerScoreController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetRankingByScore(IGetSellersScoresHnadler getSellersScoresHnadler)
        {
            return Ok(await getSellersScoresHnadler.Handle());
        }
    }
}
