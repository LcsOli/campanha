using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerScore.WriteOnly
{
    public class SellerScoreWriteOnlyRepository : ISellerScoreWriteOnlyRepository
    {
        private CampaingContextDb _context;
        public SellerScoreWriteOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task AddAsync(List<Entity.SellerScore> entities)
        {
            await _context.AddRangeAsync(entities);
        }
    }
}
