using Campaign.Pooling.DTO.Response.Order;
using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.Repositories.Order.ReadOnly;
using Campaign.Pooling.Handlers.Order.GetOrdersDetail.Validator;

namespace Campaign.Pooling.Handlers.Order.GetOrdersDetail
{
    public class GetOrdersDetailHandler : IGetOrdersDetailHandler
    {
        private readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        public GetOrdersDetailHandler(IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository)
        {
            _orderDetailReadOnlyRepository = orderDetailReadOnlyRepository;
        }

        public async Task<List<OrderDetailResponse>> Handle(GetOrdersDetailCommand cmd)
        {
            new CommandValidator()
                .Validate(cmd);

            var ordersDetails = await _orderDetailReadOnlyRepository.GetByPromotionCode(cmd.PromotionCode);

            new OrdersFindedValidator()
                .Validate(ordersDetails);

            return ordersDetails;
        }
    }
}
