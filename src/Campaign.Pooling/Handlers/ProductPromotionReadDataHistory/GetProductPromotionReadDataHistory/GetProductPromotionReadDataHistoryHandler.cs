using Entity = Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Get;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly;

namespace Campaign.Pooling.Handlers.ProductPromotionHistory.GetLastProductPromotionsHistory
{
    public class GetProductPromotionReadDataHistoryHandler : IGetProductPromotionReadDataHistoryHandler
    {
        private readonly IProductPromotionReadDataHistoryRepositorie _productPromotionReadDataHistoryRepositorie;
        public GetProductPromotionReadDataHistoryHandler(IProductPromotionReadDataHistoryRepositorie productPromotionReadDataHistoryRepositorie)
        {
            _productPromotionReadDataHistoryRepositorie = productPromotionReadDataHistoryRepositorie;
        }

        public async Task<bool> Handle(ProductPromotionWasReadCommand cmd)
        {
            return await _productPromotionReadDataHistoryRepositorie.Exists(cmd.promotionCode);
        }

        public async Task<Entity.ProductPromotionReadDataHistory?> Handle(GetProductPromotionReadHistoryCommand cmd)
        {
            return await _productPromotionReadDataHistoryRepositorie.Get(cmd.promotionCode);
        }
    }
}