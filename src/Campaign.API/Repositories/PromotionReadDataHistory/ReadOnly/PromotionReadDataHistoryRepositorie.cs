using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Product = Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.API.Repositories.PromotionReadDataHistory.ReadOnly;

namespace Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly
{
    public class PromotionReadDataHistoryRepositorie : IPromotionReadDataHistoryRepositorie
    {
        private readonly CampaingContextDb _context;
        public PromotionReadDataHistoryRepositorie(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Product.ProductPromotionSummary>> GetAll()
        {
            return await (
                            from h in _context.ProductPromotionReadDataHistories
                            join pmc in _context.ProductPromotionSummaries on h.PromotionCode equals pmc.Id
                            select pmc

                          ).OrderBy(x => x.Id)
                           .ToListAsync();
        }
    }
}
