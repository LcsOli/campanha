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

        public async Task<List<Entity.SellerScoreProductsSummary>> GetByIds(int promotionCode, int[] OrdersIds, int[] productsIds)
        {
            var query = from s in _context.SellerScoreProductsSummaries
                        join r in _context.OrderProductRemoveds on new { s.ProductId, s.CustomerId, s.SellerId } equals new { r.ProductId, r.CustomerId, r.SellerId }
                        where
                            OrdersIds.Contains(r.OrderId) &&
                            s.PromotionCode == promotionCode &&
                            productsIds.Contains(s.ProductId) 
                        select s;

            return await query.ToListAsync();
        }


        public async Task<List<Entity.SellerScoreProductsSummary>> GetByPromotionCode(int promotionCode)
        {
            return await _context.SellerScoreProductsSummaries.Where(x => x.PromotionCode == promotionCode).ToListAsync();
        }
    }
}
