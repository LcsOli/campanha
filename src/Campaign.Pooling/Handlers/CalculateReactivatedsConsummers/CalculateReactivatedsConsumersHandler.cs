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
            var sellersQuantityReactivateds = await _orderSummaryReadOnlyRepository.GetSellersIdsThatReactivatedConsumers(cmd.ConsumersIds, cmd.PromotionCode);

            //TODO - Pensar em uma forma de deixar a quantidade de pontos a serem somados flexiveis a alterações sem a necessidade de alterar o código fonte.
            sellersQuantityReactivateds.ForEach(sellerQuantityReactivated =>
            {
                var sellerScoreToUpdate = cmd.SellersScores.FirstOrDefault(seller => seller.SellerId == sellerQuantityReactivated.SellerId);
                sellerScoreToUpdate?.UpdateScore(sellerQuantityReactivated.QtyConsumers * _pointsToAdd);
                sellerScoreToUpdate?.UpdateQtyReactivateds((short)sellerQuantityReactivated.QtyConsumers);
            });
        }
    }
}
