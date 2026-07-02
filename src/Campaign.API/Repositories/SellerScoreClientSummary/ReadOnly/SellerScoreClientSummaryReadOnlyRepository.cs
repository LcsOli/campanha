using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.Enums.SellerScoreConsumerType;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.API.Repositories.SellerScoreClientSummary.ReadOnly
{
    public class SellerScoreClientSummaryReadOnlyRepository : ISellerScoreClientSummaryReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerScoreClientSummaryReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<SellerScoreClientsSummary>> GetByFilters(int size,
                                                                        int page,
                                                                        int sellerId,
                                                                        int? customerId,
                                                                        int promotionCode,
                                                                        CustomerSalesEventType? customerSalesEventType)
        {
            var query = _context.SellerScoreClientsSummaries.Where(x =>
                                                                        x.SellerId == sellerId &&
                                                                        x.PromotionCode == promotionCode &&
                                                                        (customerId == null || x.CustomerId == customerId) &&
                                                                        (customerSalesEventType == null || x.CustomerSalesEventType == customerSalesEventType)
                                                                  ).Skip((page - 1) * size).Take(size);
            return await query.ToListAsync();
        }

        public async Task<decimal> GetByFiltersCount(int sellerId,
                                                     int promotionCode,
                                                     CustomerSalesEventType? customerSalesEventType)
        {
            var query = _context.SellerScoreClientsSummaries.Where(x =>
                                                                        x.SellerId == sellerId &&
                                                                        x.PromotionCode == promotionCode &&
                                                                        (customerSalesEventType == null || x.CustomerSalesEventType == customerSalesEventType)
                                                                  ).Select(x => x.CustomerId);

            return Math.Ceiling((decimal)await query.CountAsync());
        }

        public async Task<int> CountByEventType(int sellerId,
                                                int promotionCode,
                                                CustomerSalesEventType customerSalesEventType)
        {
            var query = _context.SellerScoreClientsSummaries.Where(x =>
                                                                        x.SellerId == sellerId &&
                                                                        x.PromotionCode == promotionCode &&
                                                                        x.CustomerSalesEventType == customerSalesEventType
                                                                  ).Select(x => x.CustomerId);
            return await query.CountAsync();
        }
    }
}
