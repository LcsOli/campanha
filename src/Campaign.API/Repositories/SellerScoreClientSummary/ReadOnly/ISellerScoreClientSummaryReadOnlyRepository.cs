using Campaign.Shared.Enums.SellerScoreConsumerType;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.API.Repositories.SellerScoreClientSummary.ReadOnly
{
    public interface ISellerScoreClientSummaryReadOnlyRepository
    {
        Task<List<SellerScoreClientsSummary>> GetByFilters(int size,
                                                           int page,
                                                           int sellerId,
                                                           int? customerId,
                                                           int promotionCode,
                                                           CustomerSalesEventType? customerSalesEventType);

        Task<decimal> GetByFiltersCount(int sellerId,
                                        int promotionCode,
                                        CustomerSalesEventType? customerSalesEventType);
    }
}
