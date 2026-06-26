using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
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
                                                                                int? customer,
                                                                                int? productId,
                                                                                int promotionCode)
        {
            var query = _context.SellerScoreProductsSummaries.Where(x =>
                                                                         x.SellerId == sellerId &&
                                                                         x.PromotionCode == promotionCode &&
                                                                         (productId == null || x.ProductId == productId) &&
                                                                         (customer == null || x.CustomerId == customer)

                                                                   ).Skip((page - 1) * size).Take(size);
            return await query.ToListAsync();
        }
    }
}
