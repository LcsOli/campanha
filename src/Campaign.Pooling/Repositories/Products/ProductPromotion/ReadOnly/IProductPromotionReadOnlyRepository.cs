using Product = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.Products.ProductPromotion.ReadOnly
{
    public interface IProductPromotionReadOnlyRepository
    {
        Task<List<Product.ProductPromotion>> GetByPromotionCode(int promotionCode);
    }
}
