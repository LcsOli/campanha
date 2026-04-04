using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculateReactivatedsConsummers
{
    public class CalculateReactivatedsConsumersHandler : ICalculateReactivatedsConsumersHandler
    {
        private readonly IOrderSummaryReadOnlyRepository _orderSummaryReadOnlyRepository;
        public CalculateReactivatedsConsumersHandler(IOrderSummaryReadOnlyRepository orderSummaryReadOnlyRepository)
        {
            _orderSummaryReadOnlyRepository = orderSummaryReadOnlyRepository;
        }

        public async Task Handle(CalculateReactivatedsConsumersCommand cmd)
        {
            var sellersIds = await _orderSummaryReadOnlyRepository.GetReactivatedClients(cmd.ConsumersIds,
                                                                                           promotionCode: 202501,
                                                                                           initOfYear: new DateTime(2025, 01, 01),
                                                                                           campaignInitIn: new DateTime(2025, 06, 01),
                                                                                           campaignEndIn: new DateTime(2025, 10, 31),
                                                                                           periodStart: new DateTime(2025, 06, 01),
                                                                                           periodEnd: new DateTime(2025, 06, 07));

            var sellersScoresToUpdate = cmd.SellersScores.Where(s => sellersIds.Contains(s.Id)).ToList();

            //TODO - Pensar em uma forma de deixar a quantidade de pontos a serem somados flexiveis a alterações sem a necessidade de alterar o código fonte.
            sellersScoresToUpdate.ForEach(sellerScore => sellerScore.UpdateScore(2000));
        }
    }
}
