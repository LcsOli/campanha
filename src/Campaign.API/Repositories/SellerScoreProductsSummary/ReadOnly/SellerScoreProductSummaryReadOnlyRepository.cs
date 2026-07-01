using Campaign.API.DTO.SellerScoreProductSummary.Response;
using Campaign.Shared.DataBaseContext.Entities;
using Microsoft.EntityFrameworkCore;
using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.API.Repositories.SellerScoreProductsSummary.ReadOnly
{
    public class SellerScoreProductSummaryReadOnlyRepository : ISellerScoreProductSummaryReadOnlyRepository
    {
        private readonly CampaingContextDb _context;

        public SellerScoreProductSummaryReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.SellerScoreProductsSummary>> GetByFilters(int size,
                                                                                int page,
                                                                                int sellerId,
                                                                                int? productId,
                                                                                int? customerId,
                                                                                int promotionCode)
        {

            var productsIds = await GetOnlyProductsIdsByFilters(size, page, sellerId, productId, customerId, promotionCode);

            var query = _context.SellerScoreProductsSummaries.Where(x => x.SellerId == sellerId &&
                                                                         (customerId == null || x.CustomerId == customerId) &&
                                                                         productsIds.Contains(x.ProductId));

            return await query.ToListAsync();
        }

        public async Task<SellerScoreProductSummaryResponse.Resume> GetResumeByFilters(int sellerId,
                                                                                      int? productId,
                                                                                      int? customerId,
                                                                                      int promotionCode)
        {
            var query = _context.SellerScoreProductsSummaries.Select(x => new
            {
                x.Points,
                x.SellerId,
                x.ProductId,
                x.CustomerId,
                x.PromotionCode
            });

            query = query.Where(x => x.SellerId == sellerId &&
                                     x.PromotionCode == promotionCode &&
                                     (productId == null || x.ProductId == productId) &&
                                     (customerId == null || x.CustomerId == customerId));

            var result = await query.ToListAsync();

            var totalScore = result.Sum(x => x.Points);
            var qtyCustomers = result.DistinctBy(x => x.CustomerId).Count();

            return new(totalScore, qtyCustomers);
        }

        public async Task<decimal> GetByFiltersCount(int sellerId,
                                                     int? customer,
                                                     int? productId,
                                                     int promotionCode)
        {
            var query = _context.SellerScoreProductsSummaries.Where(x =>
                                                                          x.SellerId == sellerId &&
                                                                          x.PromotionCode == promotionCode &&
                                                                          (productId == null || x.ProductId == productId) &&
                                                                          (customer == null || x.CustomerId == customer)

                                                                     ).Select(x => x.ProductId)
                                                                      .Distinct();

            return Math.Ceiling((decimal)await query.CountAsync());
        }

        private async Task<int[]> GetOnlyProductsIdsByFilters(int size,
                                                              int page,
                                                              int sellerId,
                                                              int? productId,
                                                              int? customerId,
                                                              int promotionCode)
        {
            return await _context.SellerScoreProductsSummaries.Where(x =>
                                                                          x.SellerId == sellerId &&
                                                                          x.PromotionCode == promotionCode &&
                                                                          (productId == null || x.ProductId == productId) &&
                                                                          (customerId == null || x.CustomerId == customerId)

                                                                     ).Select(x => x.ProductId)
                                                                      .Distinct()
                                                                      .Skip((page - 1) * size).Take(size)
                                                                      .ToArrayAsync();
        }
    }
}
