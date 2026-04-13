using Campaign.Pooling.Commands.Calculate;

namespace Campaign.Pooling.Handlers.CalculateCoupons
{
    public class CalculateCouponsHandler : ICalculateCouponsHandler
    {
        public void Handle(CalculateCouponsCommand cmd)
        {
            cmd.SellerScore.ForEach(sellerScore =>
            {
                var coupons = (short)(sellerScore.CouponsByScore + sellerScore.CouponsByRevenue);

                if (coupons > 0)
                    sellerScore.UpdateCoupons(coupons);
            });
        }
    }
}