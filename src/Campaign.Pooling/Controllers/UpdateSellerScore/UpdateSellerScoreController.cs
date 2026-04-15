using Microsoft.AspNetCore.Mvc;
using Campaign.Pooling.Orchestrators.MainOrchestrator;

namespace Campaign.Pooling.Controllers.UpdateSellerScore
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateSellerScoreController : ControllerBase
    {
        [HttpPatch]
        public async Task<IActionResult> Update([FromQuery] int promotionCode, 
                                                [FromServices] IMainOrchestrator mainOrchestrator)
        {
            await mainOrchestrator.Execute(promotionCode);
            return Ok();
        }
    }
}
