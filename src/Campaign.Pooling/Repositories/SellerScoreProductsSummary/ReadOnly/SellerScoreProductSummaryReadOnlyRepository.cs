using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Processor.API.Repositories.SellerScoreProductsSummary.ReadOnly
{
    public class SellerScoreProductSummaryReadOnlyRepository : ISellerScoreProductSummaryReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerScoreProductSummaryReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.SellerScoreProductsSummary>> GetByIds(int promotionCode, int[] productsIds, int[] sellersIds, int[] customersIds)
        {
            return await _context.SellerScoreProductsSummaries.Where(x => 
                                                                          sellersIds.Contains(x.SellerId) &&
                                                                          x.PromotionCode == promotionCode &&
                                                                          productsIds.Contains(x.ProductId) &&
                                                                          customersIds.Contains(x.CustomerId)

                                                              ).ToListAsync();
        }
    }
}
