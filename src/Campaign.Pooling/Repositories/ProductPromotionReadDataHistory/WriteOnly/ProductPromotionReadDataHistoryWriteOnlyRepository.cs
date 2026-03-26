using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.WriteOnly
{
    public class ProductPromotionReadDataHistoryWriteOnlyRepository : IProductPromotionReadDataHistoryWriteOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public ProductPromotionReadDataHistoryWriteOnlyRepository(CampaingContextDb contexts)
        {
            _context = contexts;
        }

        public async Task AddAsync(Entity.ProductPromotionReadDataHistory entity)
        {
            await _context.ProductPromotionReadDataHistories.AddAsync(entity);
        }
    }
}
