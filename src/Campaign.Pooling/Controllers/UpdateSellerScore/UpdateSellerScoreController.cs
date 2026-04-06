using Microsoft.AspNetCore.Mvc;
using Campaign.Pooling.DTO.Request;
using Campaign.Pooling.Orchestrators.MainOrchestrator;

namespace Campaign.Pooling.Controllers.UpdateSellerScore
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateSellerScoreController : ControllerBase
    {
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] ProcessSellerScore request,
                                                [FromServices] IMainOrchestrator mainOrchestrator)
        {
            await mainOrchestrator.Execute(request.PromotionCode, 
                                           request.DtWeekToStartProcess, 
                                           request.DtWeekToStopProcess);
            return Ok();
        }
    }
}
