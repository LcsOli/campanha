using Campaign.Pooling.DTO.Response.Order;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.Summaries.Create
{
    public record RegisterSellerScoreProductSummaryCommand(int PromotionCode,
                                                             List<OrderDetailResponse> OrdersDetails,
                                                             List<Entity.SellerScore> SellersScores);
}
