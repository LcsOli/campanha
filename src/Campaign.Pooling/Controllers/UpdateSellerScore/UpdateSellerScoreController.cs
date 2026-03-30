using Microsoft.AspNetCore.Mvc;
using Campaign.Pooling.DTO.Request;
using Campaign.Pooling.Orchestrators.MainOrchestrator;

namespace Campaign.Pooling.Controllers.UpdateSellerScore
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateSellerScoreController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateSellerScoreRequest request,
                                                [FromServices] IMainOrchestrator mainOrchestrator)
        {
            await mainOrchestrator.Execute(request.PromotionCode, request.InitIn, request.EndIn);
            return Ok();
        }
    }
}
