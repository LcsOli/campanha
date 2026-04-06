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
            var sellersIds = await _orderSummaryReadOnlyRepository.GetSellersIdsThatRegisteredsConsumers(cmd.ConsumersIds, 
                                                                                                         cmd.PromotionCode, 
                                                                                                         cmd.DtWeekToStopProcess, 
                                                                                                         cmd.DtWeekToStartProcess);

            var sellersScoresToUpdate = cmd.SellersScores.Where(s => sellersIds.Contains(s.Id)).ToList();

            //TODO - Pensar em uma forma de deixar a quantidade de pontos a serem somados flexiveis a alterações sem a necessidade de alterar o código fonte.
            sellersScoresToUpdate.ForEach(sellerScore => sellerScore.UpdateScore(_pointsToAdd));
        }
    }
}
