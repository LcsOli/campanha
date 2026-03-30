using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerScore.ReadOnly
{
    public class SellerScoreReadOnlyRepository : ISellerScoreReadOnlyRepository
    {
        private readonly CampaingContextDb _context;

        public SellerScoreReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<int>> GetSellersInserteds(int[] SellersIds)
        {
            return await _context.SellerScores.Select(s => s.SellerId)
                                              .Where(sellerId => SellersIds.Contains(sellerId))
                                              .ToListAsync();
        }

        public async Task<List<Entity.SellerScore>> GetAll()
        {
            return await _context.SellerScores.ToListAsync();
        }
    }
}
