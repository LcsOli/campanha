using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Pooling.Handlers.CalculateScorePoints.Validator;
using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

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

            var ordersDetails = await _orderDetailReadOnlyRepository.GetByPromotionCode(cmd.PromotionCode);

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var ordersByClient = ordersDetails.Where(o => o.SellerId == sellerScore.SellerId)
                                                  .GroupBy(o => o.ConsumerId)
                                                  .Select(o => new
                                                  {
                                                      CustomerId = o.Key,
                                                      Orders = o.DistinctBy(p => p.ProductId).ToList()
                                                  }).ToList();

                ordersByClient.ForEach(o => sellerScore.UpdateScore((decimal)o.Orders.Sum(o => o.ProductPromotionPoints)!));

                CalculateCouponsByScore(sellerScore);
            });
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
