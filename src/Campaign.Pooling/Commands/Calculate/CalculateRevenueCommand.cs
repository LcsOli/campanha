using EntitySeller = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.Calculate
{
    public record CalculateRevenueCommand(int PromotionCode, List<EntitySeller.SellerScore> SellersScore);
}
