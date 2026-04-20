using Campaign.Pooling.Commands.Calculate;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.CalculateCoupons
{
    public class CalculateCouponsHandler : ICalculateCouponsHandler
    {
        public void Handle(CalculateCouponsCommand cmd)
        {
            cmd.SellerScore.ForEach(sellerScore =>
            {
                var coupons = CouponsByScore(sellerScore);
                
                coupons += (short)(sellerScore.RevenueMonth1 >= sellerScore!.RevenueTarget ? 1 : 0);
                coupons += (short)(sellerScore.RevenueMonth2 >= sellerScore!.RevenueTarget ? 1 : 0);
                coupons += (short)(sellerScore.RevenueMonth3 >= sellerScore!.RevenueTarget ? 1 : 0);
                coupons += (short)(sellerScore.RevenueMonth4 >= sellerScore!.RevenueTarget ? 1 : 0);
                coupons += (short)(sellerScore.RevenueMonth5 >= sellerScore!.RevenueTarget ? 1 : 0);

                sellerScore.UpdateCoupons(coupons);
            });
        }

        private short CouponsByScore(Entity.SellerScore sellerScore)
        {
            const int _scoreToValidate = 500_000;
            return  (short)(sellerScore.Score / _scoreToValidate);
        }
    }
}