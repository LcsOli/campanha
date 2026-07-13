using Campaign.Pooling.Commands.ProductPromotionReadHistory.Create;

namespace Campaign.Pooling.Handlers.PromotionReadDataHistory.RegisterNewHistory
{
    public interface IRegisterProductPromotionReadDataHistoryHandler
    {
        Task Handle(RegisterProductPromotionReadDataHistoryCommand cmd);
    }
}
