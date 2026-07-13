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
                var clients = cmd.OrdersDetails.Where(x => x.SellerId == sellerScore.SellerId)
                                                      .GroupBy(x => x.ConsumerId)
                                                      .Select(x => new
                                                      {
                                                          CustomerId = x.Key,
                                                          Orders = x.DistinctBy(p => p.ProductId).ToList()
                                                      }).ToList();

                if (clients.Count <= 0)
                    return;

                var points = clients.SelectMany(x => x.Orders)
                                    .Sum(x => x.ProductPromotionPoints);

                var pointsByClients = points * clients.Count;

                sellerScore.UpdateScore((decimal)pointsByClients!);
            });
        }
    }
}