using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculateReactivatedsConsummers
{
    public class CalculateReactivatedsConsumersHandler : ICalculateReactivatedsConsumersHandler
    {
        private readonly int _pointsToAdd = 2000;

        private readonly IOrderSummaryReadOnlyRepository _orderSummaryReadOnlyRepository;
        public CalculateReactivatedsConsumersHandler(IOrderSummaryReadOnlyRepository orderSummaryReadOnlyRepository)
        {
            _orderSummaryReadOnlyRepository = orderSummaryReadOnlyRepository;
        }

        public async Task Handle(CalculateReactivatedsConsumersCommand cmd)
        {
            var sellersIds = await _orderSummaryReadOnlyRepository.GetReactivatedClients(cmd.ConsumersIds,
                                                                                         cmd.PromotionCode,
                                                                                         cmd.DtWeekToStopProcess,
                                                                                         cmd.DtWeekToStartProcess);

            var sellersScoresToUpdate = cmd.SellersScores.Where(s => sellersIds.Contains(s.Id)).ToList();

            //TODO - Pensar em uma forma de deixar a quantidade de pontos a serem somados flexiveis a alterações sem a necessidade de alterar o código fonte.
            sellersScoresToUpdate.ForEach(sellerScore => sellerScore.UpdateScore(_pointsToAdd));
        }
    }
}
