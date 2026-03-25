using Product = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.Products.ProductPromotionReadDataHistory.ReadOnly
{
    public interface IProductPromotionReadDataHistoryRepositorie
    {
        Task<DateTime?> GetDateOfMostRecent();
        Task<Product.ProductPromotionReadDataHistory?> GetLastPromotionCodeCreated();
    }
}
