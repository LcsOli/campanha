using Campaign.Pooling.DTO.Response.Get;
using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.CalculateScoreByProduct
{
    public record CalculateScoreByProductCommand(List<OrderDetailResponse> OrdersDetails,
                                                 List<EntitySellerScore.SellerScore> SellersScores);
}
