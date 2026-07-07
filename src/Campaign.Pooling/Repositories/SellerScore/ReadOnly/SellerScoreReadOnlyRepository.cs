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

        public async Task<List<int>> GetRegisteredsById(int[] ids)
        {
            return await _context.SellerScores.Select(x => x.SellerId)
                                              .Where(id => ids.Contains(id))
                                              .ToListAsync();
        }

        public async Task<List<Entity.SellerScore>> GetAll()
        {
            return await _context.SellerScores.ToListAsync();
        }

        public async Task<List<Entity.SellerScore>> GetByIds(int[] ids)
        {
            return await _context.SellerScores
                                 .Where(x => ids.Contains(x.SellerId))
                                 .ToListAsync();
        }
    }
}
