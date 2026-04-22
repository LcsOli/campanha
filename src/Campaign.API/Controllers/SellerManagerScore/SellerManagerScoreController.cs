using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Campaign.API.Handlers.SellerManager.GetRevenue;

namespace Campaign.API.Controllers.SellerManagerScore
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerManagerScoreController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetRevenue(IGetSellerManagerScoreRevenueHandler getSellerManagerScoreRevenueHandler)
        {
            return Ok(await getSellerManagerScoreRevenueHandler.Handle());
        }
    }
}
