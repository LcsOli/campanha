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

                if (ordersByClient.Count <= 0)
                    return;

                ordersByClient.ForEach(o => sellerScore.UpdateScore((decimal)o.Orders.Sum(o => o.ProductPromotionPoints)!));
            });
        }
    }
}
