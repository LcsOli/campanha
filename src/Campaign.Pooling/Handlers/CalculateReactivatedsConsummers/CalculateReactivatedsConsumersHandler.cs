using Campaign.Pooling.Commands.Consumers.Get;

namespace Campaign.Pooling.Handlers.CalculateReactivatedsConsummers
{
    public class CalculateReactivatedsConsumersHandler : ICalculateReactivatedsConsumersHandler
    {
        public void Handle(CalculateReactivatedsConsumersCommand cmd)
        {
            cmd.SellersScores.ForEach(sellerScore =>
            {
                var reactivateds = cmd.ReactivatedsConsumers.Where(x => x.SellerId == sellerScore.SellerId);

                var qty = reactivateds.Count();

                if (qty <= 0) return;

                sellerScore.UpdateScore(qty * cmd.Points);
                sellerScore.UpdateQtyReactivateds((short)qty);
            });
        }
    }
}
