using Campaign.Pooling.Repositories.SellerScore.ReadOnly;
using Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderCanceled.Create;

namespace Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderCanceled
{
    public class ScoreByOrderCanceledHandler : IScoreByOrderCanceledHandler
    {
        private readonly ISellerScoreReadOnlyRepository _sellerScoreReadOnlyRepository;
        public ScoreByOrderCanceledHandler(ISellerScoreReadOnlyRepository sellerScoreReadOnlyRepository)
        {
            _sellerScoreReadOnlyRepository = sellerScoreReadOnlyRepository;
        }

        public async Task Handle(CalculateCommand cmd)
        {
            var ordersValids = cmd.Orders.GroupBy(x => new { x.SellerId, x.ConsumerId, x.OrderId })
                                         .Select(x => new
                                         {
                                             x.Key.OrderId,
                                             x.Key.SellerId,
                                             x.Key.ConsumerId,
                                             Orders = x.DistinctBy(p => p.ProductId)
                                         });

            var canceleds = ordersValids.SelectMany(x => x.Orders)
                                        .Where(x => x.CanceledIn != null && x.CanceledIn.Value > x.PromotionProcessedIn)
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

            var qtyConsumers = cmd.Orders.Where(x => x.CanceledIn == null || x.CanceledIn.Value > x.PromotionProcessedIn)
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

            }).ToList();

            var sellersIds = scoreCanceleds.Select(x => x.SellerId);

            if (!sellersIds.Any())
                return;

            var sellersScores = await _sellerScoreReadOnlyRepository.GetByIds([.. sellersIds]);

            sellersScores.ForEach(x =>
            {
                var score = scoreCanceleds.Single(y => y.SellerId == x.SellerId).ScoreCanceleds;
                x.SetScoreProductsCanceledsOrders(score!.Value);
            });

            //TODO - Criar handler específico para estas inserções

            //var ordersIds = ordersValids.Select(x => x.OrderId);
            //var productsCanceledsIds = canceleds.SelectMany(x => x.Products).Select(x => x.ProductId);

            //var productsResume = await _sellerScoreProductSummaryReadOnlyRepository.GetByIds(cmd.PromotionCode, [.. ordersIds], [.. productsCanceledsIds]);
            //productsResume.ForEach(x => x.SetCanceled());
        }
    }
}
