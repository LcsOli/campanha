using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.DTO.Response.Order;

namespace Campaign.Pooling.Handlers.Order.GetOrdersDetail
{
    public interface IGetOrdersDetailHandler
    {
        Task<List<OrderDetailResponse>> Handle(GetOrdersDetailCommand cmd);
    }
}
