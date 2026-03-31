using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Product = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotion.ReadOnly
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

        public async Task<List<Product.ProductPromotionSummary>> GetByProductsIdsAndPromotionCode(int[] productsIds, int promotionCode)
        {
            var productsPromotionSummary = await _context.ProductPromotions
                                  .Select(p => new
                                  {
                                      p.Id,
                                      p.PromotionCode,
                                      p.ProductId,
                                      p.QuantityPointsGoals
                                  })
                                  .Where(p => p.PromotionCode == promotionCode && productsIds.Contains(p.ProductId))
                                  .ToListAsync();

            return [.. productsPromotionSummary.Select(p => new Product.ProductPromotionSummary(p.Id, p.ProductId, p.PromotionCode, p.QuantityPointsGoals))];
        }

        public async Task<bool> Exists(int promotionCode)
        {
            //TODO - Não é possível simplificar a busca com AnyAsync pois a oracle não compreende a palavra chave TRUE/FALSE.

            var exists = await _context.ProductPromotions.FirstOrDefaultAsync(p => p.PromotionCode == promotionCode);

            if (exists != null)
                return true;

            return false;
        }
    }
}
