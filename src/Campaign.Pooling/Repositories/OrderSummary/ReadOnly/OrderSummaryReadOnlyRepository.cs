using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public class OrderSummaryReadOnlyRepository : IOrderSummaryReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public OrderSummaryReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<int[]> GetReactivatedClients(int[] clientsIds,
                                                       int promotionCode,
                                                       DateTime periodEnd,
                                                       DateTime initOfYear,
                                                       DateTime periodStart,
                                                       DateTime campaignEndIn,
                                                       DateTime campaignInitIn)
        {
            campaignInitIn = campaignInitIn.Date;
            campaignEndIn = campaignEndIn.Date;

            initOfYear = initOfYear.Date;

            periodStart = periodStart.Date;
            periodEnd = periodEnd.Date;

            var query = from os in _context.OrderSummaries
                         join s in _context.Sellers on os.SellerId equals s.Id
                         join od in _context.OrderDetails on os.Id equals od.Id
                         join p in _context.ProductPromotions on od.ProductId equals p.ProductId
                         join c in _context.Customers on os.CustomerId equals c.Id
                         where
                              s.SellerType == 'R' &&
                              (
                                os.DateOfSale.Date >= campaignInitIn &&
                                os.DateOfSale.Date <= campaignEndIn
                              ) &&
                              (

                                from oss in _context.OrderSummaries
                                where
                                    oss.CustomerId == c.Id &&
                                    oss.DateOfSale.Date < initOfYear
                                select 1

                              ).Any() &&
                              !(

                                from oss in _context.OrderSummaries
                                where
                                    oss.CustomerId == c.Id &&
                                    (
                                        oss.DateOfSale.Date >= initOfYear &&
                                        oss.DateOfSale.Date <= campaignInitIn
                                    )
                                select 1

                              ).Any() &&

                              (
                                from oss in _context.OrderSummaries
                                where
                                    oss.CustomerId == c.Id &&
                                    oss.DateOfSale >= campaignInitIn &&
                                    oss.DateOfSale <= campaignEndIn
                                group oss by oss.CustomerId into g
                                where
                                    g.Min(os => os.DateOfSale.Date) >= periodStart &&
                                    g.Min(os => os.DateOfSale.Date) <= periodEnd
                                select 1


                              ).Any()
                         group os by os.SellerId into g
                         select 
                            g.Key;

            return await query.ToArrayAsync();
        }
    }
}
