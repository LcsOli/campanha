using Campaign.Pooling.Commands.Orders.Get;
using Entity = Campaign.Shared.DataBaseContext.Entities.Order;

namespace Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail
{
    public interface IGetOrdersDetailHandler
    {
        Task<List<Entity.OrderDetail>> Handle(GetOrdersDetailCommand cmd);
    }
}
