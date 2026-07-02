using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Campaign.API.Commands.SellerManager.Get;
using Campaign.API.Handlers.SellerManager.GetRevenue;
using Campaign.API.Handlers.SellerManagerScore.TargetManager;

namespace Campaign.API.Controllers.SellerManager
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "manager")]
    public class SellerManagerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRevenue(ISellerManagerScoreRevenueHandler sellerManagerScoreRevenueHandler)
        {
            return Ok(await sellerManagerScoreRevenueHandler.Handle());
        }

        [HttpGet("seller/{sellerScoreId}/target-revenue")]
        public async Task<IActionResult> TargetRevenue([FromRoute] int sellerScoreId,
                                                       [FromServices] ISellerManagerRevenueTargetHandler sellerManagerRevenueTargetHandler)
        {
            var command = new GetTargetRevenueCommand(sellerScoreId);
            return Ok(await sellerManagerRevenueTargetHandler.Handle(command));
        }
    }
}
