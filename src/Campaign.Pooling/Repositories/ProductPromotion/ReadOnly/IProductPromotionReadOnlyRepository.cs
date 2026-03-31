using Product = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotion.ReadOnly
{
    public interface IProductPromotionReadOnlyRepository
    {
        Task<List<Product.ProductPromotion>> GetByPromotionCode(int promotionCode);
        Task<bool> Exists(int promotionCode);
        Task<List<Product.ProductPromotionSummary>> GetByProductsIdsAndPromotionCode(int[] productsIds, int promotionCode);
    }
}
