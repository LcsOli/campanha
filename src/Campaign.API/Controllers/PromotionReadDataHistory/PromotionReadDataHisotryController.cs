using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Campaign.API.Handlers.PromotionReadDataHistory;
using Campaign.API.Commands.PromotionReadDataHistory.Get;

namespace Campaign.API.Controllers.PromotionReadDataHistory
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class PromotionReadDataHisotryController: ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> All([FromServices] IPromotionReadDataHistoryHandler promotionReadDataHistoryHandler)
        {
            return Ok(await promotionReadDataHistoryHandler.Handle(new GetAllReadHistoryCommand()));
        }
    }
}
