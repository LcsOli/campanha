using Campaign.Shared.DataBaseContext.Entities.Order;

namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderDetailReadOnlyRepository
    {
        Task<List<OrderDetail>> GetByIdAndDateInitAndEnd(int[] productsIds, DateTime initIn, DateTime endIn);
        Task<List<OrderDetail>> GetByPromotionCodeAndDateInitAndEnd(int promotionCode, DateTime initIn, DateTime endIn);
    }
}
