using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculatePositivatedsConsummers
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
            var sellersQuantityReactivateds = await _orderSummaryReadOnlyRepository.GetSellersIdsThatRegisteredsConsumers(cmd.ConsumersIds, cmd.PromotionCode);

            sellersQuantityReactivateds.ForEach(sellerQuantityReactivated =>
            {
                var sellerScoreToUpdate = cmd.SellersScores.FirstOrDefault(s => s.Id == sellerQuantityReactivated.SellerId);

                if (sellerScoreToUpdate is not null)
                    sellerScoreToUpdate.UpdateScore(sellerQuantityReactivated.QtyReactivatedsConsumers * _pointsToAdd);
            });
        }
    }
}
