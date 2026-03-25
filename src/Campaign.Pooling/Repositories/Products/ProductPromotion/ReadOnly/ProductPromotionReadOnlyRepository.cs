using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Product = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.Products.ProductPromotion.ReadOnly
{
    public class ProductPromotionReadOnlyRepository : IProductPromotionReadOnlyRepository
    {
        private readonly CampaingContextDb _context;

        public ProductPromotionReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Product.ProductPromotion>> GetByPromotionCode(int promotionCode)
        {
            return await _context.ProductPromotions.Where(p => p.PromotionCode == promotionCode).ToListAsync();
        }
    }
}
