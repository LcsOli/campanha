using Campaign.Pooling.Commands.Consumers.Get;

namespace Campaign.Pooling.Handlers.CalculateRegisteredsConsummers
{
    public class CalculateRegisteredsConsumersHandler : ICalculateRegisteredsConsumersHandler
    {
        public void Handle(CalculateRegisteredsConsumersCommand cmd)
        {
            cmd.SellersScores.ForEach(sellerScore =>
            {
                var registereds = cmd.RegisteredsConsumers.Where(x => x.SellerId == sellerScore.SellerId);

                var qty = registereds.Count();

                if (qty <= 0) return;

                sellerScore.UpdateScore(qty * cmd.Points);
                sellerScore.UpdateQtyRegistereds((short)qty);
            });
        }
    }
}
