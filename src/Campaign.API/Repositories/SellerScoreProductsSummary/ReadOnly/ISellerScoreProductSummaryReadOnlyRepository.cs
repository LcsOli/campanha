using Campaign.API.DTO.SellerScoreProductSummary.Response;
using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.API.Repositories.SellerScoreProductsSummary.ReadOnly
{
    public interface ISellerScoreProductSummaryReadOnlyRepository
    {
        Task<List<Entity.SellerScoreProductsSummary>> GetByFilters(int size,
                                                                   int page,
                                                                   int sellerId,
                                                                   int? productId,
                                                                   int? customerId,
                                                                   int promotionCode);


        Task<SellerScoreProductSummaryResponse.Resume> GetResumeByFilters(int sellerId,
                                                                          int? productId,
                                                                          int? customerId,
                                                                          int promotionCode);

        Task<decimal> GetByFiltersCount(int sellerId,
                                        int? productId,
                                        int? customerId,
                                        int promotionCode);
    }
}
