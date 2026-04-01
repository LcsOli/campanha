using Campaign.Pooling.DTO.Response.Get;
using Entity = Campaign.Shared.DataBaseContext.Entities.Order;

namespace Campaign.Pooling.Repositories.OrderDetail.ReadOnly
{
    public interface IOrderDetailReadOnlyRepository
    {
        Task<List<Entity.OrderDetail>> GetByIdAndDateInitAndEnd(int[] productsIds, DateTime initIn, DateTime endIn);
        Task<List<OrderDetailResponse>> GetByPromotionCodeAndDateInitAndEnd(int promotionCode, DateTime initIn, DateTime endIn);
    }
}
