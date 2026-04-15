using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculateReactivatedsConsummers
{
    public class CalculateReactivatedsConsumersHandler : ICalculateReactivatedsConsumersHandler
    {
        private readonly int _pointsToAdd = 2_000;

        private readonly IOrderSummaryReadOnlyRepository _orderSummaryReadOnlyRepository;
        public CalculateReactivatedsConsumersHandler(IOrderSummaryReadOnlyRepository orderSummaryReadOnlyRepository)
        {
            _orderSummaryReadOnlyRepository = orderSummaryReadOnlyRepository;
        }

        public async Task Handle(CalculateReactivatedsConsumersCommand cmd)
        {
            var clientsReactivatedsBySellers = await _orderSummaryReadOnlyRepository.GetSellersIdsThatReactivatedConsumers(cmd.PromotionCode);

            clientsReactivatedsBySellers.ForEach(clientsReactivatedsBySeller =>
            {
                var sellerScore = cmd.SellersScores.FirstOrDefault(seller => seller.SellerId == clientsReactivatedsBySeller.SellerId);

                sellerScore?.UpdateScore(clientsReactivatedsBySeller.QtyConsumers * _pointsToAdd);
                sellerScore?.UpdateQtyReactivateds((short)clientsReactivatedsBySeller.QtyConsumers);
            });
        }
    }
}
