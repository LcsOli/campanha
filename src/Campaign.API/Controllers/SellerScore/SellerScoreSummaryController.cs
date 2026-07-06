using Microsoft.AspNetCore.Mvc;
using Campaign.Shared.Extensions.Enums;
using Microsoft.AspNetCore.Authorization;
using Campaign.API.Commands.SellerScore.Get;
using Campaign.Shared.Enums.SellerScoreConsumerType;
using Campaign.API.Handlers.SellerScore.GetSellerScoreClientSummary;
using Campaign.API.Handlers.SellerScore.GetSellerScoreProductSummary;

namespace Campaign.API.Controllers.SellerScore
{
    [ApiController]
    [Authorize(Roles = "manager, user")]
    [Route("api/seller-score/{SellerId}/summary")]
    public class SellerScoreSummaryController : ControllerBase
    {
        [HttpGet("by-product")]
        public async Task<IActionResult> Products([FromQuery] int Page,
                                                  [FromQuery] int? Size,
                                                  [FromRoute] int SellerId,
                                                  [FromQuery] int? ProductId,
                                                  [FromQuery] int? CustomerId,
                                                  [FromQuery] int PromotionCode,
                                                  [FromServices] ISellerScoreProductSummaryHandler sellerScoreProductSummaryHandler
            )
        {
            var command = new GetSellerScoreProductSummaryCommand(Page: Page,
                                                                  SellerId: SellerId,
                                                                  ProductId: ProductId,
                                                                  CustomerId: CustomerId,
                                                                  PromotionCode: PromotionCode,
                                                                  Size: Size!.HasValue ? Size.Value : 10);

            return Ok(await sellerScoreProductSummaryHandler.Handle(command));
        }

        [HttpGet("by-customer")]
        public async Task<IActionResult> Customers([FromQuery] int Page,
                                                   [FromQuery] int? Size,
                                                   [FromRoute] int SellerId,
                                                   [FromQuery] int? CustomerId,
                                                   [FromQuery] string? EventType,
                                                   [FromQuery] int PromotionCode,
                                                   [FromServices] ISellerScoreClientSummaryHandler sellerScoreClientSummaryHandler)
        {

            CustomerSalesEventType? eventType = !string.IsNullOrEmpty(EventType) ? 
                                                    EventType.ToUpper().EnumDescriptionTranslatedToNumericValue<CustomerSalesEventType>() : null;

            var command = new GetSellerScoreClientSummaryCommand(Page: Page,
                                                                 SellerId: SellerId,
                                                                 CustomerId: CustomerId,
                                                                 PromotionCode: PromotionCode,
                                                                 CustomerSalesEventType: eventType,
                                                                 Size: Size!.HasValue ? Size.Value : 10);

            return Ok(await sellerScoreClientSummaryHandler.Handle(command));
        }

    }
}
