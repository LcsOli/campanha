using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerManager.WriteOnly
{
    public class SellerManagerScoreWriteOnlyRepository : ISellerManagerScoreWriteOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerManagerScoreWriteOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public void UpdateAll(List<Entity.SellerManagerScore> entities)
        {
            _context.SellerManagers.UpdateRange(entities);
        }
    }
}
