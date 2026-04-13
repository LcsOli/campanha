using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Repositories.SellerScore.WriteOnly;
using Campaign.Pooling.Handlers.CalculateScorePoints.Validator;
using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

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

                ordersByClient.ForEach(o => sellerScore.UpdateScore((decimal)o.Orders.Sum(o => o.ProductPromotionPoints)!));

                CalculateCouponsByScore(sellerScore);
            });

            //TODO - Verificar a necessidade de utilizar update. Caso realmente for necessário, chamar o update no final da rotina para pegar todas as atualizações da entidade.
            _sellerScoreWriteOnlyRepository.Update(cmd.SellersScores);
        }

        private void CalculateCouponsByScore(EntitySellerScore.SellerScore sellerScore)
        {
            //TODO - Verificar a possibilidade de extrair para um handler, o calculo de cupons por score.

            const int _scoreToValidate = 500_000;

            var couponsToUpdate = (short)(sellerScore.Score / _scoreToValidate);

            if (couponsToUpdate > sellerScore.Coupons)
                sellerScore.UpdateCouponsByScore();
        }
    }
}
