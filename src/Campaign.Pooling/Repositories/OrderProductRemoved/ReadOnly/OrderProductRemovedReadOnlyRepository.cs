using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Processor.API.DTO.Response.OrderProductRemoved.Items.Get;

namespace Campaign.Pooling.Repositories.OrderProductRemoved.ReadOnly
{
    public class OrderProductRemovedReadOnlyRepository : IOrderProductRemovedReadOnlyRepository
    {
        public readonly CampaingContextDb _context;
        public OrderProductRemovedReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }
        public async Task<List<OrderProductItemsRemoved>> GetByOrdersIds(int promotionCode, long[] ordersIds)
        {
            var query = from r in _context.OrderProductRemoveds
                        join p in _context.ProductPromotions on r.ProductId equals p.ProductId
                        join ph in _context.ProductPromotionReadDataHistories on p.PromotionCode equals ph.PromotionCode
                        where
                            r.QtyProductHeld == 0 &&
                            r.ReplicationIn > ph.ReadAt &&
                            ordersIds.Contains(r.OrderId) &&
                            p.PromotionCode == promotionCode
                        select new OrderProductItemsRemoved(r.OrderId,
                                                            r.SellerId,
                                                            p.ProductId,
                                                            r.CustomerId,
                                                            p.QuantityPointsGoals!.Value);

            return await query.ToListAsync();
        }
    }
}
