using Campaign.Pooling.Commands.SellerManager.Update;

namespace Campaign.Pooling.Handlers.UpdateCurrentRevenueSellerManager
{
    public interface IUpdateCurrentRevenueSellerManagerHandler
    {
        Task Handle(UpdateCurrentRevenueSellerManagerCommand cmd);
    }
}
