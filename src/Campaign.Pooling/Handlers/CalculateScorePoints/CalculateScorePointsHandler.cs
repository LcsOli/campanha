using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Repositories.SellerScore.WriteOnly;
using Campaign.Pooling.Handlers.CalculateScorePoints.Validator;

namespace Campaign.Pooling.Handlers.CalculateScoreByProduct
{
    public class CalculateScorePointsHandler : ICalculateScorePointsHandler
    {
        private readonly ISellerScoreWriteOnlyRepository _sellerScoreWriteOnlyRepository;
        public CalculateScorePointsHandler(ISellerScoreWriteOnlyRepository sellerScoreWriteOnlyRepository)
        {
            _sellerScoreWriteOnlyRepository = sellerScoreWriteOnlyRepository;
        }

        public void Handle(CalculateScoreByProductCommand cmd)
        {
            new DataToCalcIsDefinedValidator()
                .Validate(cmd);

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var ordersByClient = cmd.OrdersDetails.Where(o => o.SellerId == sellerScore.SellerId)
                                                      .GroupBy(o => o.CustomerId)
                                                      .Select(o =>
                                                      {
                                                          return new
                                                          {
                                                              CustomerId = o.Key,
                                                              Orders = o.DistinctBy(p => p.ProductId).ToList()
                                                          };
                                                      }).ToList();

                ordersByClient.ForEach(o =>
                {
                    var productsIds = o.Orders.Select(p => p.ProductId);

                    var points = cmd.ProductsPromotions.Where(p => productsIds.Contains(p.ProductId))
                                                   .Sum(p => p.QuantityPointsGoals);

                    sellerScore.UpdateScore(points!.Value);
                });
            });

            _sellerScoreWriteOnlyRepository.Update(cmd.SellersScores);
        }
    }
}
