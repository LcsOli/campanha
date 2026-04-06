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

        public async Task<int[]> GetSellersIdsThatReactivatedConsumers(int[] clientsIds,
                                                                    int promotionCode,
                                                                    DateTime dtWeekToStopProcess,
                                                                    DateTime dtWeekToStartProcess)
        {

            dtWeekToStartProcess = dtWeekToStartProcess.Date;
            dtWeekToStopProcess = dtWeekToStopProcess.Date;

            var yearOfCampaign = new DateTime(dtWeekToStartProcess.Year, 01, 01).Date;

            var query = from ps in _context.ProductPromotionSummaries
                        join p in _context.ProductPromotions on ps.Id equals p.PromotionCode
                        join od in _context.OrderDetails on p.ProductId equals od.ProductId
                        join s in _context.Sellers on od.SellerId equals s.Id
                        join c in _context.Customers on od.CustomerId equals c.Id
                        where
                             clientsIds.Contains(c.Id) &&
                             c.RegisteredAt.Date < yearOfCampaign &&
                             ps.Id == promotionCode &&
                             s.SellerType == 'R' &&
                             (
                               od.DateOfSale.Date >= ps.InitIn &&
                               od.DateOfSale.Date <= ps.EndIn
                             ) &&
                             (
                               from oss in _context.OrderSummaries
                               where
                                   oss.CustomerId == c.Id &&
                                   oss.DateOfSale.Date < yearOfCampaign
                               select 1

                             ).Take(1).Any() &&
                             !(

                               from oss in _context.OrderSummaries
                               where
                                   oss.CustomerId == c.Id &&
                                   (
                                       oss.DateOfSale.Date >= yearOfCampaign &&
                                       oss.DateOfSale.Date < ps.InitIn.Date
                                   )
                               select 1

                             ).Take(1).Any() &&
                             (

                               from oss in _context.OrderSummaries
                               where
                                   oss.CustomerId == c.Id &&
                                   (
                                    oss.DateOfSale >= ps.InitIn.Date &&
                                    oss.DateOfSale <= ps.EndIn.Date
                                   )
                               group oss by oss.CustomerId into g
                               where
                                   g.Min(os => os.DateOfSale.Date) >= dtWeekToStartProcess &&
                                   g.Min(os => os.DateOfSale.Date) <= dtWeekToStopProcess
                               select 1

                             ).Any()
                        group od by od.SellerId into g
                        select
                           g.Key;

            return await query.ToArrayAsync();
        }

        public async Task<int[]> GetSellersIdsThatRegisteredsConsumers(int[] clientsIds,
                                                                    int promotionCode,
                                                                    DateTime dtWeekToStopProcess,
                                                                    DateTime dtWeekToStartProcess)
        {
            dtWeekToStartProcess = dtWeekToStartProcess.Date;
            dtWeekToStopProcess = dtWeekToStopProcess.Date;

            var query = from ps in _context.ProductPromotionSummaries
                        join p in _context.ProductPromotions on ps.Id equals p.PromotionCode
                        join od in _context.OrderDetails on p.ProductId equals od.ProductId
                        join s in _context.Sellers on od.SellerId equals s.Id
                        join c in _context.Customers on od.CustomerId equals c.Id
                        where
                             s.SellerType == 'R' &&
                             clientsIds.Contains(c.Id) &&
                             ps.Id == promotionCode &&
                             c.RegisteredAt.Date >= ps.InitIn &&
                             (
                               od.DateOfSale.Date >= ps.InitIn &&
                               od.DateOfSale.Date <= ps.EndIn
                             ) &&
                             (

                               from oss in _context.OrderSummaries
                               where
                                   oss.CustomerId == c.Id &&
                                   (
                                    oss.DateOfSale >= ps.InitIn.Date &&
                                    oss.DateOfSale <= ps.EndIn.Date
                                   )
                               group oss by oss.CustomerId into g
                               where
                                   g.Min(os => os.DateOfSale.Date) >= dtWeekToStartProcess &&
                                   g.Min(os => os.DateOfSale.Date) <= dtWeekToStopProcess
                               select 1

                             ).Any()
                        group od by od.SellerId into g
                        select
                           g.Key;

            return await query.ToArrayAsync();
        }
    }
}
