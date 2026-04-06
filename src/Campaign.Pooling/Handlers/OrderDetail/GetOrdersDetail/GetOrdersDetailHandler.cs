using Campaign.Pooling.DTO.Response.Get;
using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
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

        public async Task<List<OrderDetailResponse>> Handle(GetOrdersDetailCommand cmd)
        {
            new CommandValidator()
                .Validate(cmd);

            var ordersDetails = await _orderDetailReadOnlyRepository.GetByPromotionCodeAndDateInitAndEnd(cmd.promotionCode, cmd.DtWeekToStartProcess, cmd.DtWeekToStopProcess);

            new OrdersFindedValidator()
                .Validate(ordersDetails);

            return ordersDetails;
        }
    }
}
