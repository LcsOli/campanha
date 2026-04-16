using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly
{
    public class ProductPromotionSummaryReadOnlyRepository : IProductPromotionSummaryReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public ProductPromotionSummaryReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<Entity.ProductPromotionSummary?> GetByPromotionCode(int promotionCode)
        {
            return await _context.ProductPromotionSummaries.FirstOrDefaultAsync(p => p.Id == promotionCode);
        }

        public async Task<Entity.ProductPromotionSummary?> GetByPeriod(DateTime period)
        {
            var productToIgnore = int.Parse(period.Year.ToString().PadRight(6, '0'));

            return await _context.ProductPromotionSummaries.FirstOrDefaultAsync(p => p.Id != productToIgnore && 
                                                                               (p.InitIn.Date <= period.Date && p.EndIn.Date >= period.Date));
        }
    }
}
