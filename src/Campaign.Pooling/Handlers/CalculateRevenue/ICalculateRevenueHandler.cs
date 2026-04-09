using Campaign.Pooling.Commands.Calculate;

namespace Campaign.Pooling.Handlers.CalculateRevenueTarget
{
    public interface ICalculateRevenueHandler
    {
        Task Handle(CalculateRevenueCommand cmd);
    }
}
