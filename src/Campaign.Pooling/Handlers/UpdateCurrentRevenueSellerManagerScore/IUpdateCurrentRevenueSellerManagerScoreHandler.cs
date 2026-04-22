using Campaign.Pooling.Commands.SellerManager.Update;

namespace Campaign.Pooling.Handlers.UpdateCurrentRevenueSellerManager
{
    public interface IUpdateCurrentRevenueSellerManagerScoreHandler
    {
        Task Handle(UpdateCurrentRevenueSellerManagerCommand cmd);
    }
}
