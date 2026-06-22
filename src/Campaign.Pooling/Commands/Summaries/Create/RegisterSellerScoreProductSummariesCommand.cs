using Campaign.Pooling.DTO.Response.Order;
using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.Summaries.Create
{
    public record RegisterSellerScoreProductSummariesCommand(int PromotionCode,
                                                             List<OrderDetailResponse> OrdersDetails,
                                                             List<EntitySellerScore.SellerScore> SellersScores);
}
