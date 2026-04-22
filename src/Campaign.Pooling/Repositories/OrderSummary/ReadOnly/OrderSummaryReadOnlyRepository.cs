using Campaign.Pooling.DTO.Response.Seller;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.WriteOnly;
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

        public async Task<List<SellersQuantityConsumersResponse>> GetSellersIdsThatReactivatedConsumers(int promotionCode)
        {
            var query = _context.Database.SqlQuery<SellersQuantityConsumersResponse>($@"
                        SELECT 
                            codusur AS SellerId,
                            COUNT(codcli) AS QtyConsumers
                        FROM
                        (
                            SELECT
                                c.codusur,
                                c.codcli
                            FROM
                                cf_campanha_rca_score crs
                                JOIN pcpedc c ON c.codusur = crs.rca_id
                                JOIN pcusuari u ON c.codusur = u.codusur
                                JOIN pcpedi i ON i.numped = c.numped
                                JOIN pcpromoi p ON p.codprod = i.codprod
                                JOIN pcpromoc pc ON pc.codpromocao = p.codpromocao
                                JOIN pcclient client ON c.codcli = client.codcli
                                JOIN pcpromoc pcgeral ON pcgeral.codpromocao = TO_NUMBER(EXTRACT(YEAR FROM pc.dtinicio) || '00')
                            WHERE
                                u.tipovend = 'R' AND
                                p.codpromocao = {promotionCode} AND
                                TO_CHAR(client.dtcadastro, 'yyyy-MM-DD') < CONCAT(EXTRACT(YEAR FROM pcgeral.dtinicio), '-01-01') AND
                                c.data BETWEEN pcgeral.dtinicio AND pcgeral.dtfim AND
                                EXISTS(
                                        SELECT 
                                                1
                                        FROM 
                                            pcpedc cc 
                                        WHERE 
                                            cc.codcli = c.codcli AND
                                            TO_CHAR(cc.data, 'yyyy-MM-DD') < EXTRACT(YEAR FROM pcgeral.dtinicio) || '-01-01'
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
                                                    TO_CHAR(cc.data, 'YYYY-MM-DD') >= EXTRACT(YEAR FROM pc.dtinicio) || '-01-01' AND
                                                    cc.data < pcgeral.dtinicio
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
                                            cc.data >= pcgeral.dtinicio AND
                                            cc.data <= pcgeral.dtfim
                                        GROUP BY
                                            cc.codcli
                                        having
                                            min(cc.data) >= pc.dtinicio AND
                                            min(cc.data) <= pc.dtfim
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

        public async Task<List<SellersQuantityConsumersResponse>> GetSellersIdsThatRegisteredsConsumers(int promotionCode)
        {
            var query = _context.Database.SqlQuery<SellersQuantityConsumersResponse>($"""
                SELECT 
                    codusur AS SellerId,
                    COUNT(codcli) AS QtyConsumers
                FROM
                (
                    SELECT
                        c.codusur,
                        c.codcli
                    FROM
                        cf_campanha_rca_score crs
                        JOIN pcpedc c ON c.codusur = crs.rca_id
                        JOIN pcusuari u ON c.codusur = u.codusur
                        JOIN pcpedi i ON i.numped = c.numped
                        JOIN pcpromoi p ON p.codprod = i.codprod
                        JOIN pcpromoc pc ON pc.codpromocao = p.codpromocao
                        JOIN pcclient client ON c.codcli = client.codcli
                        JOIN pcpromoc pcgeral ON pcgeral.codpromocao = TO_NUMBER(CONCAT(EXTRACT(YEAR FROM pc.dtinicio), '00'))
                    WHERE
                        u.tipovend = 'R' AND
                        p.codpromocao = {promotionCode} AND
                        client.dtcadastro >= pcgeral.dtinicio AND
                        (
                            c.data >= pcgeral.dtinicio AND
                            c.data <= pcgeral.dtfim
                        ) AND
                        EXISTS(
                              SELECT
                                  cc.codcli
                              FROM
                                  pcpedc cc
                              WHERE
                                  cc.codcli = c.codcli AND
                                  cc.data >= pcgeral.dtinicio AND
                                  cc.data <= pcgeral.dtfim
                              GROUP BY
                                  cc.codcli
                              HAVING
                                  MIN(cc.data) >= pc.dtinicio AND
                                  MIN(cc.data) <= pc.dtfim
                        )
                    GROUP BY 
                        c.codusur,
                        c.codcli
                )
                GROUP BY 
                    codusur

                """);

            return await query.ToListAsync();
        }
    }
}
