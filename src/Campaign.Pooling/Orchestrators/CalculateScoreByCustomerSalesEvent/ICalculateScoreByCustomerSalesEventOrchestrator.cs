using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Orchestrators.CalculateScoreByCustomerSalesEvent
{
    public interface ICalculateScoreByCustomerSalesEventOrchestrator
    {
        Task Execute(int promotionCode, List<Entity.SellerScore> SellersScores);
    }
}
