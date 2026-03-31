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
            var query = from o in _context.OrderDetails
                        join sc in _context.SellerScores on o.SellerId equals sc.SellerId
                        where
                            productsIds.Contains(o.ProductId) &&
                            (o.DateOfSale.Date >= initIn.Date && o.DateOfSale.Date <= endIn.Date)
                        select new OrderDetail(o.Id,
                                               o.SellerId,
                                               o.Price,
                                               o.ProductId,
                                               o.CustomerId,
                                               o.Quantity,
                                               o.DateOfSale);
            return await query.ToListAsync();
        }

        public async Task<List<OrderDetail>> GetByPromotionCodeAndDateInitAndEnd(int promotionCode, DateTime initIn, DateTime endIn)
        {
            var query = from os in _context.OrderSummaries
                        join o in _context.OrderDetails on os.Id equals o.Id
                        join sc in _context.SellerScores on os.SellerId equals sc.SellerId
                        join pm in _context.ProductPromotions on o.ProductId equals pm.ProductId
                        where
                            pm.PromotionCode == promotionCode &&
                            (os.DateOfSale.Date >= initIn.Date && os.DateOfSale.Date <= endIn.Date)
                        select new OrderDetail(o.Id,
                                               o.SellerId,
                                               o.Price,
                                               o.ProductId,
                                               o.CustomerId,
                                               o.Quantity,
                                               o.DateOfSale);
            return await query.ToListAsync();
        }
    }
}
