using Campaign.Pooling.Commands.Calculate;

namespace Campaign.Pooling.Handlers.CalculateCoupons
{
    public interface ICalculateCouponsHandler
    {
        void Handle(CalculateCouponsCommand cmd);
    }
}
