using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.Sellers.Seller.ReadOnly
{
    public class SellerReadOnlyRepository : ISellerReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.Seller>> GetSellersByIds(int[] sellersIds)
        {
            return await _context.Sellers.Where(s => sellersIds.Contains(s.Id)).ToListAsync();
        }
    }
}
