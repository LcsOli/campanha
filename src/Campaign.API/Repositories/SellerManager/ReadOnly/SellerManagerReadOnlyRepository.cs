using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.SellerManager.ReadOnly
{
    public class SellerManagerReadOnlyRepository : ISellerManagerReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerManagerReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.SellerManagerScore>> GetAll()
        {
            return await _context.SellerManagers.ToListAsync();
        }
    }
}
