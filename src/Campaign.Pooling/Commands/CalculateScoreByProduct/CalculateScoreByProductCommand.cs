using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.CalculateScoreByProduct
{
    public record CalculateScoreByProductCommand(int PromotionCode, List<EntitySellerScore.SellerScore> SellersScores);
}
