using Campaign.Pooling.Repositories.Order.ReadOnly;
using Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderCanceled.Create;
using Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderCanceled
{
    public class ScoreByOrderCanceledHandler : IScoreByOrderCanceledHandler
    {
        private readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        public ScoreByOrderCanceledHandler(IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository)
        {
            _orderDetailReadOnlyRepository = orderDetailReadOnlyRepository;
        }

        public async Task Handle(CalculateCommand cmd)
        {
            var orders = await _orderDetailReadOnlyRepository.GetAllByPromotionCode(cmd.PromotionCode);
            var sellersIds = orders.Select(x => x.SellerId);


            var ordersValids = orders.GroupBy(x => new { x.SellerId, x.ConsumerId })
                                     .Select(x => new
                                     {
                                         x.Key.SellerId,
                                         x.Key.ConsumerId,
                                         Orders = x.DistinctBy(p => p.ProductId)
                                     });

            var canceleds = ordersValids.SelectMany(x => x.Orders)
                                        .Where(x => x.CanceledIn != null && x.CanceledIn.Value >= x.PromotionEndIn)
                                        .GroupBy(x => x.SellerId)
                                        .Select(x => new
                                        {
                                            SellerId = x.Key,
                                            Products = x.Select(x => new
                                            {
                                                x.ProductId,
                                                x.ConsumerId,
                                                x.ProductPromotionPoints

                                            }).DistinctBy(x => new { x.ProductId, x.ConsumerId })
                                        });

            var qtyConsumers = orders.Where(x => x.CanceledIn == null || x.CanceledIn.Value >= x.PromotionEndIn)
                                     .GroupBy(x => x.SellerId)
                                     .Select(x => new
                                     {
                                         SellerId = x.Key,
                                         QtyConsumers = x.Select(y => y.ConsumerId).Distinct().Count()
                                     });

            var scoreCanceleds = canceleds.Select(x =>
            {
                var qtyConsumersBySeller = qtyConsumers.Single(y => y.SellerId == x.SellerId).QtyConsumers;

                return new
                {
                    x.SellerId,
                    ScoreCanceleds = x.Products.Sum(x => x.ProductPromotionPoints) * qtyConsumersBySeller
                };
            });
        }
    }
}
