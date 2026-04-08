using Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.Calculate
{
    public record CalculateCouponsCommand(List<SellerScore> SellerScore);
}
