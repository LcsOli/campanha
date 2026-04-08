using Campaign.Pooling.Commands.Calculate;

namespace Campaign.Pooling.Handlers.CalculateCoupons
{
    public class CalculateCouponsHandler : ICalculateCouponsHandler
    {
        private readonly int _scoreToValidate = 500_000;
        public CalculateCouponsHandler()
        {

        }

        public async Task Handle(CalculateCouponsCommand cmd)
        {
            cmd.SellerScore.ForEach(sellerScore =>
            {
                sellerScore.UpdateCoupons((short)(sellerScore.Score / _scoreToValidate));
            });
        }
    }
}
