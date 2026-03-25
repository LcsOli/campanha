using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.Pooling.Repositories.Sellers.Seller.ReadOnly
{
    public class SellerReadOnlyRepository : ISellerReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task GetSellersByIds(int[] sellersIds)
        {
            return await _context.Sellers.Where(s => sellersIds.Contains(s.Id)).ToListAsync();
        }
    }
}
