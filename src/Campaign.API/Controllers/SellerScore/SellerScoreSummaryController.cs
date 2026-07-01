using Microsoft.AspNetCore.Mvc;
using Campaign.API.Commands.SellerScore.Get;
using Campaign.API.Handlers.SellerScore.GetSellerScoreProductSummary;

namespace Campaign.API.Controllers.SellerScore
{
    [ApiController]
    [Route("api/seller-score/{SellerId}/summary")]
    public class SellerScoreSummaryController : ControllerBase
    {
        [HttpGet("by-product")]
        public async Task<IActionResult> Products([FromQuery] int Page,
                                                  [FromRoute] int SellerId,
                                                  [FromQuery] int? ProductId,
                                                  [FromQuery] int? CustomerId,
                                                  [FromQuery] int PromotionCode,
                                                  [FromServices] ISellerScoreProductSummaryHandler sellerScoreProductSummaryHandler,
                                                  [FromQuery] int? Size = 10
            )
        {
            var command = new GetSellerScoreProductSummaryCommand(Page: Page,
                                                                  Size: Size!.Value, 
                                                                  SellerId: SellerId,
                                                                  ProductId: ProductId,
                                                                  CustomerId: CustomerId,
                                                                  PromotionCode: PromotionCode);

            return Ok(await sellerScoreProductSummaryHandler.Handle(command));
        }
    }
}
