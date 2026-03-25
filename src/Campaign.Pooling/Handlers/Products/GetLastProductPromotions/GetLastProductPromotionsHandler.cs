using Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Pooling.Repositories.Products.ProductPromotionReadDataHistory.ReadOnly;

namespace Campaign.Pooling.Handlers.Products.GetLastProductPromotions
{
    public class GetLastProductPromotionsHandler : IGetLastProductPromotionsHandler
    {
        private readonly IProductPromotionReadDataHistoryRepositorie _productPromotionReadDataHistoryRepositorie;
        public GetLastProductPromotionsHandler(IProductPromotionReadDataHistoryRepositorie productPromotionReadDataHistoryRepositorie)
        {
            _productPromotionReadDataHistoryRepositorie = productPromotionReadDataHistoryRepositorie;
        }

        public async Task<ProductPromotionReadDataHistory?> Handle()
        {
           return await _productPromotionReadDataHistoryRepositorie.GetLastPromotionCodeCreated();
        }
    }
}
