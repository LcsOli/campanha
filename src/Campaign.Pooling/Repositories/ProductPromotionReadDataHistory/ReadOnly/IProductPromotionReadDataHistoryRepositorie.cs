using Product = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly
{
    public interface IProductPromotionReadDataHistoryRepositorie
    {
        Task<DateTime?> GetDateOfMostRecent();
        Task<bool> Exists(int promotionCode);
        Task<Product.ProductPromotionReadDataHistory?> Get(int promotionCode);
    }
}
