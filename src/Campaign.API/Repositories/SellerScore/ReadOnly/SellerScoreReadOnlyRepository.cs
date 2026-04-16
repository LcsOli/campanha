using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.ProductPromotion.ReadOnly
{
    public class SellerScoreReadOnlyRepository : ISellerScoreReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerScoreReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<Entity.SellerScore?> GetBySellerId(int sellerId)
        {
            return await _context.SellerScores.FirstOrDefaultAsync(p => p.SellerId == sellerId);
        }
    }
}
