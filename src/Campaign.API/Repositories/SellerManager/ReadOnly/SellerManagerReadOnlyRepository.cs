using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.API.Repositories.SellerManager.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.PromotionReadDataHistory.ReadOnly
{
    public class SellerManagerReadOnlyRepository : ISellerManagerReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerManagerReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.SellerManagers
                                 .Select(x => x.SellerId)
                                 .CountAsync(sellerId => sellerId == id) > 0;
        }

        public async Task<List<Entity.SellerManagerScore>> GetAll()
        {
            return await _context.SellerManagers.ToListAsync();
        }

        public async Task<decimal> GetRevenueTarget(int id)
        {
            var sellersManagersIds = await _context.SellerManagers.Where(x => x.SellerId == id)
                                                                  .Select(x => x.Code)
                                                                  .ToArrayAsync();

            return await _context.SellerScores.Select(x => new { x.SellerManagerId, x.RevenueTarget })
                                              .Where(x => sellersManagersIds.Contains(x.SellerManagerId))
                                              .SumAsync(x => x.RevenueTarget);
        }
    }
}
