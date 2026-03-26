using Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Handlers.Products.GetLastProductPromotions
{
    public interface IGetLastProductPromotionsHistoryHandler
    {
        Task<ProductPromotionReadDataHistory?> Handle();
    }
}
