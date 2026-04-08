using Campaign.Pooling.DTO.Response.Get;
using Campaign.Shared.DataBaseContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public class OrderSummaryReadOnlyRepository : IOrderSummaryReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public OrderSummaryReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<int[]> DemoEntityFrameworkQueryGetSellersIdsThatReactivatedConsumers(int[] clientsIds,
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

        public async Task<List<SellersQuantityConsumersReactivatedsResponse>> GetSellersIdsThatReactivatedConsumers(int[] consumersIds, int promotionCode)
        {
            var query = _context.Database.SqlQuery<SellersQuantityConsumersReactivatedsResponse>($@"
                                                          SELECT 
                                                              codusur AS SellerId,
                                                              COUNT(codusur) AS QtyReactivatedsConsumers
                                                          FROM
                                                          (
                                                                SELECT
                                                                    c.codusur,
                                                                    c.codcli
                                                                FROM
                                                                    pcpedc c
                                                                    JOIN pcusuari u on c.codusur = u.codusur
                                                                    JOIN pcpedi i on i.numped = c.numped
                                                                    JOIN pcpromoi p on p.codprod = i.codprod
                                                                    JOIN pcpromoc pc on pc.codpromocao = p.codpromocao
                                                                    JOIN pcclient client on c.codcli = client.codcli
                                                                    JOIN pcpromoc pcgeral on pcgeral.codpromocao = TO_NUMBER(CONCAT(to_char(pc.dtinicio, 'yyyy'), '00'))
                                                                WHERE
                                                                    u.tipovend = 'R' AND
                                                                    c.codcli IN(
                                                                              SELECT 
                                                                                  TO_NUMBER(REGEXP_SUBSTR({string.Join(",", consumersIds)}, '[^,]+', 1, LEVEL))
                                                                              FROM 
                                                                                  dual
                                                                              CONNECT BY REGEXP_SUBSTR({string.Join(",", consumersIds)}, '[^,]+', 1, LEVEL) IS NOT NULL
                                                                    ) AND
                                                                    p.codpromocao = {consumersIds} AND
                                                                    TO_CHAR(client.dtcadastro, 'yyyy-MM-DD') < CONCAT(TO_CHAR(pcgeral.dtinicio, 'yyyy'), '-01-01') AND
                                                                    (
                                                                        TO_CHAR(c.data, 'yyyy-MM-DD') >= TO_CHAR(pcgeral.dtinicio, 'yyyy-MM-DD') AND
                                                                        TO_CHAR(c.data, 'yyyy-MM-DD') <= TO_CHAR(pcgeral.dtfim, 'yyyy-MM-DD') 
                                                                    ) AND
                                                                    EXISTS(
                                                                            SELECT 
                                                                                    1
                                                                            FROM 
                                                                                pcpedc cc 
                                                                            WHERE 
                                                                                cc.codcli = c.codcli AND
                                                                                TO_CHAR(cc.data, 'yyyy-MM-DD') < CONCAT(TO_CHAR(pcgeral.dtinicio, 'yyyy'), '-01-01')
                                                                            FETCH FIRST 1 ROW ONLY
                                                                    ) AND
                                                                    NOT EXISTS(
                                                                                SELECT 
                                                                                    1
                                                                                FROM 
                                                                                    pcpedc cc
                                                                                WHERE 
                                                                                    cc.codcli = c.codcli AND
                                                                                    (
                                                                                        TO_CHAR(cc.data, 'YYYY-MM-DD') >= CONCAT(TO_CHAR(pc.dtinicio, 'yyyy'), '-01-01') AND
                                                                                        TO_CHAR(cc.data, 'YYYY-MM-DD') < TO_CHAR(pcgeral.dtinicio, 'yyyy-MM-DD')
                                                                                    )
                                                                                FETCH FIRST 1 ROW ONLY
                                                                    ) AND
                                                                    EXISTS(
                                                                          SELECT
                                                                              cc.codcli
                                                                          FROM
                                                                              pcpedc cc
                                                                          WHERE
                                                                              cc.codcli = c.codcli AND
                                                                              TO_CHAR(cc.data, 'yyyy-MM-DD') >= TO_CHAR(pcgeral.dtinicio, 'yyyy-MM-DD') AND
                                                                              TO_CHAR(cc.data, 'yyyy-MM-DD') <= TO_CHAR(pcgeral.dtfim, 'yyyy-MM-DD') 
                                                                          GROUP BY
                                                                              cc.codcli
                                                                          having
                                                                              TO_CHAR(min(cc.data), 'yyyy-MM-DD') >= to_char(pc.dtinicio, 'yyyy-MM-DD') AND
                                                                              TO_CHAR(min(cc.data), 'yyyy-MM-DD') <= to_char(pc.dtfim, 'yyyy-MM-DD')
                                                                    )
                                                                GROUP BY 
                                                                    c.codusur,
                                                                    c.codcli
                                                          )
                                                         GROUP BY 
                                                             codusur
            ");

            return await query.ToListAsync();
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
