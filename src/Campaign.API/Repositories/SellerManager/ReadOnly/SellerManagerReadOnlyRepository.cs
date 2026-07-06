using Microsoft.EntityFrameworkCore;
using Campaign.API.DTO.SellerManager.Response;
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

        public async Task<SellerManagerRevenueInfosResponse> GetRevenueTarget(int? id)
        {
            var sellersManagersIds = await _context.SellerManagers.Where(x => id == null || x.SellerId == id)
                                                                  .Select(x => x.Code)
                                                                  .ToArrayAsync();

            var revenueBySellerManager = from sm in _context.SellerManagers.Where(x => id == null || x.SellerId == id)
                                         join sc in _context.SellerScores on sm.Code equals sc.SellerManagerId
                                         group sc by new { sm.SellerId, sm.Name } into g
                                         select new SellerManagerRevenueInfosResponse.RevenueInfo(SellerName: g.Key.Name,
                                                                                                  SellerId: g.Key.SellerId,
                                                                                                  RevenueTarget: g.Sum(x => x.RevenueTarget),
                                                                                                  CurrentRevenue: g.Sum(x => x.CurrentRevenue));
            var result = await revenueBySellerManager.ToListAsync();

            return new SellerManagerRevenueInfosResponse(result);
        }
    }
}
