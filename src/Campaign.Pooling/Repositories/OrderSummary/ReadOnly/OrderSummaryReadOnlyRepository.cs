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

        public async Task<int[]> GetReactivatedClients(int[] clientsIds, DateTime cutoffDate)
        {
            cutoffDate = cutoffDate.Date;

            var query = from o in _context.OrderSummaries
                        join c in _context.Customers on o.CustomerId equals c.Id
                        where
                            clientsIds.Contains(o.CustomerId) &&
                            c.RegisteredAt < cutoffDate &&
                            o.DateOfSale.Date < cutoffDate &&
                            (
                                from os in _context.OrderSummaries
                                where
                                    os.CustomerId == c.Id &&
                                    os.DateOfSale.Date >= cutoffDate
                                select os.Id

                            ).Count() == 1
                        
                        select 
                            o.CustomerId;

            return await query.ToArrayAsync();

        }
    }
}
