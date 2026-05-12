using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Pooling.Handlers.CalculateScorePoints.Validator;

namespace Campaign.Pooling.Handlers.CalculateScoreByProduct
{
    public class CalculateScoreByProductHandler : ICalculateScoreByProductHandler
    {
        private readonly IOrderDetailReadOnlyRepository _orderDetailReadOnlyRepository;
        public CalculateScoreByProductHandler(IOrderDetailReadOnlyRepository orderDetailReadOnlyRepository)
        {
            _orderDetailReadOnlyRepository = orderDetailReadOnlyRepository;
        }

        public async Task Handle(CalculateScoreByProductCommand cmd)
        {
            new DataToCalcIsDefinedValidator()
                .Validate(cmd);

            var orders = await _orderDetailReadOnlyRepository.GetByPromotionCode(cmd.PromotionCode);

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var clients = orders.Where(o => o.SellerId == sellerScore.SellerId)
                                                 .GroupBy(o => o.ConsumerId)
                                                 .Select(o => new
                                                 {
                                                     CustomerId = o.Key,
                                                     Orders = o.DistinctBy(p => p.ProductId).ToList()
                                                 }).ToList();

                if (clients.Count <= 0)
                    return;

                var points = clients.SelectMany(o => o.Orders)
                                    .Sum(o => o.ProductPromotionPoints);

                var pointsByClients = points * clients.Count;

                sellerScore.UpdateScore((decimal)pointsByClients!);
            });
        }
    }
}