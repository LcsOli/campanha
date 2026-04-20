using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerManager.WriteOnly
{
    public class SellerManagerWriteOnlyRepository : ISellerManagerWriteOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerManagerWriteOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public void UpdateAll(List<Entity.SellerManager> entities)
        {
            _context.SellerManagers.UpdateRange(entities);
        }
    }
}
