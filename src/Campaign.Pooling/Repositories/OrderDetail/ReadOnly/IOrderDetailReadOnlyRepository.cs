using Campaign.Pooling.DTO.Response.Order;
using Campaign.Pooling.DTO.Response.Revenue;
using Entity = Campaign.Shared.DataBaseContext.Entities.Order;

namespace Campaign.Pooling.Repositories.OrderDetail.ReadOnly
{
    public interface IOrderDetailReadOnlyRepository
    {
        Task<List<Entity.OrderDetail>> GetByIdAndDateInitAndEnd(int[] productsIds, DateTime initIn, DateTime endIn);
        Task<List<OrderDetailResponse>> GetByPromotionCode(int promotionCode);
        Task<List<TotalRevenueResponse>> CalculateCurrentRevenue(int promotionCode);
        Task<List<TotalRevenueResponse>> CalculateRevenueByMonth(DateTime init, DateTime end);
    }
}
