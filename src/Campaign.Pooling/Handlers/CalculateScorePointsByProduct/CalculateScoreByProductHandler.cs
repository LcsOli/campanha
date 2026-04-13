using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Repositories.SellerScore.WriteOnly;
using Campaign.Pooling.Handlers.CalculateScorePoints.Validator;

namespace Campaign.Pooling.Handlers.CalculateScoreByProduct
{
    public class CalculateScoreByProductHandler : ICalculateScoreByProductHandler
    {
        private readonly ISellerScoreWriteOnlyRepository _sellerScoreWriteOnlyRepository;
        public CalculateScoreByProductHandler(ISellerScoreWriteOnlyRepository sellerScoreWriteOnlyRepository)
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
                                                      .GroupBy(o => o.ConsumerId)
                                                      .Select(o => new
                                                      {
                                                          CustomerId = o.Key,
                                                          Orders = o.DistinctBy(p => p.ProductId).ToList()
                                                      }).ToList();

                ordersByClient.ForEach(o =>
                    sellerScore.UpdateScore((decimal)o.Orders.Sum(o => o.ProductPromotionPoints)!));
            });

            //TODO - Verificar a necessidade de utilizar update. Caso realmente for necessário, chamar o update no final da rotina para pegar todas as atualizações da entidade.
            _sellerScoreWriteOnlyRepository.Update(cmd.SellersScores);
        }
    }
}
