using Campaign.Pooling.Commands.ProductPromotionReadHistory.Get;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Handlers.PromotionHistory.GetLastProductPromotionsHistory
{
    public interface IGetProductPromotionReadDataHistoryHandler
    {
        Task Handle(ProductPromotionWasReadCommand cmd);
        Task<Entity.ProductPromotionReadDataHistory?> Handle(GetProductPromotionReadHistoryCommand cmd);
    }
}
