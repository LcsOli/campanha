using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.Consumers.Get
{
    public record CalculateScoreCustomerRegisteredsSalesEventCommand(int PromotionCode, List<Entity.SellerScore> SellersScores);
}
