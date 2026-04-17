using Product = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly
{
    public interface IProductPromotionReadDataHistoryRepositorie
    {
        Task<bool> Exists(int promotionCode);
        Task<Product.ProductPromotionReadDataHistory?> Get(int promotionCode);
        Task<Product.ProductPromotionReadDataHistory?> GetLast();
    }
}
