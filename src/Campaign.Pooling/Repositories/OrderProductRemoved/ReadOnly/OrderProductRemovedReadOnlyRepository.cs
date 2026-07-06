using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.OrderProductRemoved;

namespace Campaign.Pooling.Repositories.OrderProductRemoved.ReadOnly
{
    public class OrderProductRemovedReadOnlyRepository : IOrderProductRemovedReadOnlyRepository
    {
        public readonly CampaingContextDb _context;
        public OrderProductRemovedReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.OrderProductRemoved>> GetByOrdersIds(int promotionCode, int[] ordersIds)
        {
            var query = from r in _context.OrderProductRemoveds
                        join p in _context.ProductPromotions on r.ProductId equals p.ProductId
                        where
                            ordersIds.Contains(r.OrderId) &&
                            p.PromotionCode == promotionCode
                        select r;

            return await query.ToListAsync();
        }
    }
}
