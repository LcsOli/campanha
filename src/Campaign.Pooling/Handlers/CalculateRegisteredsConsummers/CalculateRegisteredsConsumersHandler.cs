using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculateRegisteredsConsummers
{
    public class CalculateRegisteredsConsumersHandler : ICalculateRegisteredsConsumersHandler
    {
        private readonly int _pointsToAdd = 10_000;

        private readonly IOrderSummaryReadOnlyRepository _orderSummaryReadOnlyRepository;
        public CalculateRegisteredsConsumersHandler(IOrderSummaryReadOnlyRepository orderSummaryReadOnlyRepository)
        {
            _orderSummaryReadOnlyRepository = orderSummaryReadOnlyRepository;
        }

        public async Task Handler(CalculateRegisteredsConsumersCommand cmd)
        {
            var sellersQuantityReactivateds = await _orderSummaryReadOnlyRepository.GetSellersIdsThatRegisteredsConsumers(cmd.PromotionCode);

            sellersQuantityReactivateds.ForEach(sellerQuantityRegistereds =>
            {
                var sellerScoreToUpdate = cmd.SellersScores.FirstOrDefault(sellerScore => sellerScore.SellerId == sellerQuantityRegistereds.SellerId);

                sellerScoreToUpdate?.UpdateScore(sellerQuantityRegistereds.QtyConsumers * _pointsToAdd);
                sellerScoreToUpdate?.UpdateQtyRegistereds((short)sellerQuantityRegistereds.QtyConsumers);
            });
        }
    }
}
