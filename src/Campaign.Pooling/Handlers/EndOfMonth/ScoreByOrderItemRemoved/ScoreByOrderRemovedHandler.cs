using Campaign.Pooling.Repositories.SellerScore.ReadOnly;
using Campaign.Pooling.Repositories.OrderProductRemoved.ReadOnly;
using Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderRemoved.Create;
using Campaign.Processor.API.Repositories.SellerScoreProductsSummary.ReadOnly;
using Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderItemRemoved.Validator;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;

namespace Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderItemRemoved
{
    public class ScoreByOrderRemovedHandler : IScoreByOrderRemovedHandler
    {
        private readonly IUnityOfWork _unitOfWork;

        private readonly ISellerScoreReadOnlyRepository _sellerScoreReadOnlyRepository;
        private readonly IOrderProductRemovedReadOnlyRepository _orderProductRemovedReadOnlyRepository;
        private readonly ISellerScoreProductSummaryReadOnlyRepository _sellerScoreProductSummaryReadOnlyRepository;

        public ScoreByOrderRemovedHandler(IUnityOfWork unitOfWork,
                                          ISellerScoreReadOnlyRepository sellerScoreReadOnlyRepository,
                                          IOrderProductRemovedReadOnlyRepository orderProductRemovedReadOnlyRepository,
                                          ISellerScoreProductSummaryReadOnlyRepository sellerScoreProductSummaryReadOnlyRepository)
        {
            _unitOfWork = unitOfWork;
            _sellerScoreReadOnlyRepository = sellerScoreReadOnlyRepository;
            _orderProductRemovedReadOnlyRepository = orderProductRemovedReadOnlyRepository;
            _sellerScoreProductSummaryReadOnlyRepository = sellerScoreProductSummaryReadOnlyRepository;
        }

        public async Task Handle(CalculateCommand cmd)
        {
            new RegisterScoreRemovedDataValidator()
                .Validate(cmd);

            var ordersValids = cmd.Orders.GroupBy(x => new { x.SellerId, x.ConsumerId, x.OrderId, x.ProductId })
                                         .Select(x => new
                                         {
                                             x.Key.OrderId,
                                             x.Key.SellerId,
                                             x.Key.ProductId,
                                             x.Key.ConsumerId
                                         });

            var ordersIds = ordersValids.Select(x => x.OrderId).Distinct();

            var productsRemoveds = await _orderProductRemovedReadOnlyRepository.GetByOrdersIds(cmd.PromotionCode, [.. ordersIds]);

            if (productsRemoveds.Count == 0)
                return;

            var scoreToRemoveBySeller = productsRemoveds.GroupBy(x => x.SellerId)
                                                        .Select(x => new
                                                        {
                                                            SellerId = x.Key,
                                                            Score = x.Sum(y => y.ProductPromotionPoints)
                                                        });

            var sellersIds = scoreToRemoveBySeller.Select(x => x.SellerId);

            var sellers = await _sellerScoreReadOnlyRepository.GetByIds([.. sellersIds]);

            sellers.ForEach(x =>
            {
                var qtyConsumers = ordersValids.Where(y => y.SellerId == x.SellerId)
                                               .Select(y => y.ConsumerId)
                                               .Distinct()
                                               .Count();

                var score = scoreToRemoveBySeller.Single(y => y.SellerId == x.SellerId).Score!.Value * qtyConsumers;

                x.SetScoreProductRemovedFromOrders(score);
            });

            var consumersIds = ordersValids.Where(y => sellersIds.Contains(y.SellerId))
                                           .Select(y => y.ConsumerId)
                                           .Distinct();

            var productsRemovedsIds = productsRemoveds.Select(x => x.ProductId).Distinct();

            var productsResume = await _sellerScoreProductSummaryReadOnlyRepository.GetByIds(cmd.PromotionCode, [.. productsRemovedsIds], [.. sellersIds], [.. consumersIds]);
            productsResume.ForEach(x => x.SetRemoved());

            await _unitOfWork.SaveAsync();
        }
    }
}