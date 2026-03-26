using Campaign.Pooling.Commands.ProductPromotionReadHistory.Create;

namespace Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory
{
    public interface IRegisterProductPromotionReadDataHistoryHandler
    {
        Task Handle(RegisterProductPromotionReadDataHistoryCommand cmd);
    }
}
