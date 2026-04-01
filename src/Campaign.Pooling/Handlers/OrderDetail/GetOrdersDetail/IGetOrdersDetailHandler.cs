using Campaign.Pooling.DTO.Response.Get;
using Campaign.Pooling.Commands.Orders.Get;

namespace Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail
{
    public interface IGetOrdersDetailHandler
    {
        Task<List<OrderDetailResponse>> Handle(GetOrdersDetailCommand cmd);
    }
}
