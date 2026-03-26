using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Product = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly
{
    public class ProductPromotionReadDataHistoryRepositorie : IProductPromotionReadDataHistoryRepositorie
    {
        private readonly CampaingContextDb _context;
        public ProductPromotionReadDataHistoryRepositorie(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<DateTime?> GetDateOfMostRecent()
        {
            //TODO - Verificar como a busca irá se comportar caso não seja encontrado nada.
            return await _context.ProductPromotionReadDataHistories.MaxAsync(p => p.ReadAt);
        }

        public async Task<Product.ProductPromotionReadDataHistory?> Get(int promotionCode)
        {
            return await _context.ProductPromotionReadDataHistories.FirstOrDefaultAsync(p => p.PromotionCode == promotionCode);
        }

        public async Task<bool> Exists(int promotionCode)
        {
            return await _context.ProductPromotionReadDataHistories.AnyAsync(p => p.PromotionCode == promotionCode);
        }

    }
}
