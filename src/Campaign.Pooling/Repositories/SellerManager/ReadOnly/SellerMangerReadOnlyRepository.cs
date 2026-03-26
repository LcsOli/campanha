using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerManager.ReadOnly
{
    public class SellerMangerReadOnlyRepository : ISellerMangerReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerMangerReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.SellerManager>> GetAll()
        {
            return await _context.SellerManagers.ToListAsync();
        }
    }
}
