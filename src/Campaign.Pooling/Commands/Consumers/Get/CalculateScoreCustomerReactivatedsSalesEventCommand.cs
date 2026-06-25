using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.Consumers.Get
{
    //TODO - Pensar na possibilidade de criar um command comum e usar como super classe para classes semelhantes a estas.
    public record CalculateScoreCustomerReactivatedsSalesEventCommand(int PromotionCode, List<Entity.SellerScore> SellersScores);
}
