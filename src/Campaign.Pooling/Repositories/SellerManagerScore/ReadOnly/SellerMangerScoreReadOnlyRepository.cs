using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerManager.ReadOnly
{
    public class SellerMangerScoreReadOnlyRepository : ISellerMangerScoreReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerMangerScoreReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.SellerManagerScore>> GetAll()
        {
            return await _context.SellerManagers.ToListAsync();
        }
    }
}
