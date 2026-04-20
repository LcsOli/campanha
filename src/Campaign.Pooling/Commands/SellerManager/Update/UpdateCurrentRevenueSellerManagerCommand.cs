using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.SellerManager.Update
{
    public record UpdateCurrentRevenueSellerManagerCommand(List<Entity.SellerScore> SellersScores);
}
