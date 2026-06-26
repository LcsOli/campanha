using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.API.Repositories.SellerScoreProductsSummary.ReadOnly
{
    public interface ISellerScoreProductSummaryReadOnlyRepository
    {
        Task<List<Entity.SellerScoreProductsSummary>> GetByFilters(int size,
                                                                   int page,
                                                                   int sellerId,
                                                                   int? customer,
                                                                   int? productId,
                                                                   int promotionCode);
    }
}
