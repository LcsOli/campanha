using Campaign.API.Commands.PromotionReadDataHistory.Get;
using Campaign.API.Handlers.PromotionReadDataHistory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
