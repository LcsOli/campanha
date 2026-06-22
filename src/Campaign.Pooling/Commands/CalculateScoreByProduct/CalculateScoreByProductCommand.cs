using Campaign.Pooling.DTO.Response.Order;
using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.CalculateScoreByProduct
{
    public record CalculateScoreByProductCommand(int PromotionCode, 
                                                 List<OrderDetailResponse> OrdersDetails,
                                                 List<EntitySellerScore.SellerScore> SellersScores);
}
