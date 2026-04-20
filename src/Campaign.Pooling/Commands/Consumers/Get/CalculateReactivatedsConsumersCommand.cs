using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.Consumers.Get
{
    public record CalculateReactivatedsConsumersCommand(int PromotionCode, List<Entity.SellerScore> SellersScores);
}
