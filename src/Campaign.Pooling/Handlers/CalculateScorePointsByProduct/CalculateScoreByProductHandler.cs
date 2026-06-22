using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScorePoints.Validator;

namespace Campaign.Pooling.Handlers.CalculateScoreByProduct
{
    public class CalculateScoreByProductHandler : ICalculateScoreByProductHandler
    {
        public void Handle(CalculateScoreByProductCommand cmd)
        {
            new DataToCalcIsDefinedValidator()
                .Validate(cmd);

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var clients = cmd.OrdersDetails.Where(o => o.SellerId == sellerScore.SellerId)
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