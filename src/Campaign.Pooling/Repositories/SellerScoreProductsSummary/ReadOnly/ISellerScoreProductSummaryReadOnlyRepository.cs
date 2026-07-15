using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Processor.API.Repositories.SellerScoreProductsSummary.ReadOnly
{
    public interface ISellerScoreProductSummaryReadOnlyRepository
    {
        Task<List<Entity.SellerScoreProductsSummary>> GetByIds(int promotionCode, long[] OrdersIds, int[] productsIds);
        Task<List<Entity.SellerScoreProductsSummary>> GetByPromotionCode(int promotionCode);
    }
}
