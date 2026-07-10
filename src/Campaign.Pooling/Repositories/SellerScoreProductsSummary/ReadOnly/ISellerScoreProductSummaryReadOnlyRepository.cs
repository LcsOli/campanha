using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Processor.API.Repositories.SellerScoreProductsSummary.ReadOnly
{
    public interface ISellerScoreProductSummaryReadOnlyRepository
    {
        Task<List<Entity.SellerScoreProductsSummary>> GetByIds(int promotionCode, int[] productsIds, int[] sellersIds, int[] customersIds);
    }
}
