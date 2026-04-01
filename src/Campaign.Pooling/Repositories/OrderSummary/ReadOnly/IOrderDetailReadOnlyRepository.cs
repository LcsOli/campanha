using Campaign.Pooling.DTO.Response.Get;
using Campaign.Shared.DataBaseContext.Entities.Order;

namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderDetailReadOnlyRepository
    {
        Task<List<OrderDetail>> GetByIdAndDateInitAndEnd(int[] productsIds, DateTime initIn, DateTime endIn);
        Task<List<OrderDetailResponse>> GetByPromotionCodeAndDateInitAndEnd(int promotionCode, DateTime initIn, DateTime endIn);
    }
}
