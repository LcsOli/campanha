using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Orchestrators.CalculateScoreByProduct
{
    public interface ICalculateScoreByProductOrchestrator
    {
        Task Execute(int promotionCode, List<EntitySellerScore.SellerScore> SellersScores);
    }
}
