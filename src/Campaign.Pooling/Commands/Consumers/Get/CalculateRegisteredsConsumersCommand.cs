using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.Consumers.Get
{
    public record CalculateRegisteredsConsumersCommand(int PromotionCode,
                                                      int[] ConsumersIds,
                                                      List<Entity.SellerScore> SellersScores);
}
