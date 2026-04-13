using Campaign.Pooling.Commands.Calculate;

namespace Campaign.Pooling.Handlers.CalculateCoupons
{
    public class CalculateCouponsHandler : ICalculateCouponsHandler
    {
        private readonly int _scoreToValidate = 500_000;

        public void Handle(CalculateCouponsCommand cmd)
        {
            cmd.SellerScore.ForEach(sellerScore =>
            {
                var couponsToUpdate = (short)(sellerScore.Score / _scoreToValidate);

                if (couponsToUpdate > sellerScore.Coupons)
                    sellerScore.UpdateCouponsByScore();
            });
        }
    }
}