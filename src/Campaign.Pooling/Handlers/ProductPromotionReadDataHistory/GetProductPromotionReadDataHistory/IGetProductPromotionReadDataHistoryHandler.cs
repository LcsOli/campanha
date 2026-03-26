using Campaign.Pooling.Commands.ProductPromotionReadHistory.Get;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Handlers.ProductPromotionHistory.GetLastProductPromotionsHistory
{
    public interface IGetProductPromotionReadDataHistoryHandler
    {
        Task<bool> Handle(ProductPromotionWasReadCommand cmd);
        Task<Entity.ProductPromotionReadDataHistory?> Handle(GetProductPromotionReadHistoryCommand cmd);
    }
}
