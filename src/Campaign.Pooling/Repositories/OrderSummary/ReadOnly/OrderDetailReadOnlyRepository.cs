using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.DataBaseContext.Entities.Order;

namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public class OrderDetailReadOnlyRepository : IOrderDetailReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public OrderDetailReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> GetByIdAndDateInitAndEnd(int[] productsIds, DateTime initIn, DateTime endIn)
        {
            return await _context.OrderDetails.Where(o => (o.DateOfSale >= initIn && o.DateOfSale <= endIn) &&
                                                                 productsIds.Contains(o.ProductId)
                                                    ).ToListAsync();


        }
    }
}
