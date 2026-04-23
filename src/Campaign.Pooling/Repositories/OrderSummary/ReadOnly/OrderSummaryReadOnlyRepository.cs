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
                            x.SellerId,
                            COUNT(x.codcli) AS QtyConsumers
                        FROM
                        (
                            SELECT
                                c.codusur AS SellerId,
                                c.codcli
                            FROM 
                                cf_campanha_rca_score crs
                                JOIN pcpedc c ON c.codusur = crs.rca_id
                                JOIN pcusuari u ON c.codusur = u.codusur
                                JOIN pcpedi i ON i.numped = c.numped
                                JOIN pcpromoi p ON p.codprod = i.codprod
                                JOIN pcpromoc pc ON pc.codpromocao = p.codpromocao
                                JOIN pcclient client ON c.codcli = client.codcli
                                JOIN pcpromoc pcgeral ON pcgeral.codpromocao = EXTRACT(YEAR FROM pc.dtinicio) * 100
                                JOIN
                                (
                                    SELECT
                                        cc.codcli,
                                        MIN(CASE
                                                WHEN cc.data < TRUNC(pcg.dtinicio, 'YEAR')
                                                THEN cc.data
                                            END
                                            ) AS dt_antes_ano,
                                        MIN(CASE
                                                WHEN 
                                                    cc.data >= TRUNC(pcg.dtinicio, 'YEAR') AND cc.data < pcg.dtinicio
                                                THEN cc.data
                                            END
                                            ) AS dt_entre_ano_e_promocao,
                                        MIN(CASE
                                                WHEN 
                                                    cc.data >= pcg.dtinicio AND cc.data <= pcg.dtfim
                                                THEN cc.data
                                            END
                                            ) AS dt_primeira_compra_periodo
                                    FROM 
                                        pcpedc cc
                                        CROSS JOIN (
                                            SELECT
                                                dtinicio,
                                                dtfim
                                            FROM pcpromoc
                                            WHERE codpromocao = 202500
                                        ) pcg
                                        GROUP BY
                                            cc.codcli
                                ) hist ON hist.codcli = c.codcli
                            WHERE
                                u.tipovend = 'R'
                                AND p.codpromocao = {promotionCode}
                                AND client.dtcadastro < TRUNC(pcgeral.dtinicio, 'YEAR')
                                AND c.data BETWEEN pcgeral.dtinicio AND pcgeral.dtfim
                                AND hist.dt_antes_ano IS NOT NULL
                                AND hist.dt_entre_ano_e_promocao IS NULL
                                AND hist.dt_primeira_compra_periodo BETWEEN pc.dtinicio AND pc.dtfim
                            GROUP BY
                                c.codusur,
                                c.codcli
                        ) x
                        GROUP BY
                            x.SellerId
            ");

            return await query.ToListAsync();
        }

        public async Task<List<SellersQuantityConsumersResponse>> GetSellersIdsThatRegisteredsConsumers(int promotionCode)
        {
            var query = _context.Database.SqlQuery<SellersQuantityConsumersResponse>($"""
                SELECT
                    x.SellerId,
                    COUNT(x.codcli) AS QtyConsumers
                FROM
                (
                    SELECT
                        c.codusur AS SellerId,
                        c.codcli
                    FROM 
                		cf_campanha_rca_score crs
                		JOIN pcpedc c ON c.codusur = crs.rca_id
                        JOIN pcusuari u ON c.codusur = u.codusur
                        JOIN pcpedi i ON i.numped = c.numped
                        JOIN pcpromoi p ON p.codprod = i.codprod
                        JOIN pcpromoc pc ON pc.codpromocao = p.codpromocao
                        JOIN pcclient client ON c.codcli = client.codcli
                        JOIN pcpromoc pcgeral ON pcgeral.codpromocao = EXTRACT(YEAR FROM pc.dtinicio) * 100
                        JOIN (
                            SELECT
                                cc.codcli,
                                MIN(cc.data) AS primeira_compra
                            FROM pcpedc cc
                            GROUP BY cc.codcli
                    ) hist ON hist.codcli = c.codcli
                    WHERE
                        u.tipovend = 'R'
                        AND p.codpromocao = {promotionCode}
                        AND client.dtcadastro >= pcgeral.dtinicio
                        AND c.data >= pcgeral.dtinicio
                        AND c.data <= pcgeral.dtfim
                        AND hist.primeira_compra >= pc.dtinicio
                        AND hist.primeira_compra <= pc.dtfim
                    GROUP BY
                        c.codusur,
                        c.codcli
                ) x
                GROUP BY
                    x.SellerId

                """);

            return await query.ToListAsync();
        }
    }
}
