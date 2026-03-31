using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Order;
using Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail.Validator;

namespace Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail
{
    public class GetOrdersDetailHandler : IGetOrdersDetailHandler
    {
        private readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        public GetOrdersDetailHandler(IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository)
        {
            _orderDetailReadOnlyRepository = orderDetailReadOnlyRepository;
        }

        public async Task<List<Entity.OrderDetail>> Handle(GetOrdersDetailCommand cmd)
        {
            new CommandValidator()
                .Validate(cmd);

            var ordersDetails = await _orderDetailReadOnlyRepository.GetByPromotionCodeAndDateInitAndEnd(cmd.promotionCode, cmd.initIn, cmd.endIn);

            new OrdersFindedValidator()
                .Validate(ordersDetails);

            return ordersDetails;
        }
    }
}
