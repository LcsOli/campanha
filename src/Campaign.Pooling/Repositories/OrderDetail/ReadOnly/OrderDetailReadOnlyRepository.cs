using Microsoft.EntityFrameworkCore;
using Campaign.Pooling.DTO.Response.Order;
using Campaign.Pooling.DTO.Response.Revenue;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Order;

namespace Campaign.Pooling.Repositories.OrderDetail.ReadOnly
{
    public class OrderDetailReadOnlyRepository : IOrderDetailReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public OrderDetailReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.OrderDetail>> GetByIdAndDateInitAndEnd(int[] productsIds, DateTime dtWeekToStartProcess, DateTime dtWeekToStopProcess)
        {
            var query = from o in _context.OrderDetails
                        join sc in _context.SellerScores on o.SellerId equals sc.SellerId
                        where
                            productsIds.Contains(o.ProductId) &&
                            (
                                o.DateOfSale.Date >= dtWeekToStartProcess.Date &&
                                o.DateOfSale.Date <= dtWeekToStopProcess.Date
                            )
                        select new Entity.OrderDetail(o.Id,
                                                      o.SellerId,
                                                      o.Price,
                                                      o.ProductId,
                                                      o.CustomerId,
                                                      o.Quantity,
                                                      o.DateOfSale);
            return await query.ToListAsync();
        }

        public async Task<List<OrderDetailResponse>> GetByPromotionCode(int promotionCode)
        {
            var query = _context.Database.SqlQuery<OrderDetailResponse>($"""
                            SELECT
                                pi.qt AS Quantity,
                                pc.numped AS OrderId,
                                pi.data AS DateOfSale,
                                pc.codusur AS SellerId,
                                pi.codprod AS ProductId,
                                pi.codcli AS ConsumerId,
                                pc.dtcancel AS CanceledIn,
                                c.cliente AS ConsumerName,
                                p.descricao AS ProductDescription,
                                pm.qtpontoscliente AS ProductPromotionPoints
                            FROM
                                cf_campanha_rca_score s
                                JOIN pcpedc pc ON pc.codusur = s.rca_id
                                JOIN pcpedi pi ON pi.numped = pc.numped
                                JOIN pcpromoi pm ON pm.codprod = pi.codprod
                                JOIN pcpromoc pmc ON pmc.codpromocao = pm.codpromocao
                                JOIN pcclient c ON c.codcli = pc.codcli
                                JOIN pcprodut p ON p.codprod = pi.codprod
                            WHERE
                                pc.dtcancel IS NULL AND
                                pm.codpromocao = {promotionCode} AND
                                (
                                    TRUNC(pc.data) >= TRUNC(pmc.dtinicio) AND
                                    TRUNC(pc.data) <= TRUNC(pmc.dtfim)
                                )
                    """);
            return await query.ToListAsync();
        }

        public async Task<List<TotalRevenueResponse>> CalculateCurrentRevenue(int promotionCode)
        {
            var query = _context.Database.SqlQuery<TotalRevenueResponse>($"""
                                SELECT
                                     c.codusur AS SellerId,
                                     SUM(i.qt * i.pvenda) AS Revenue
                                FROM
                                    cf_campanha_rca_score crs
                                    JOIN pcpedc c ON c.codusur = crs.rca_id
                                    JOIN pcpedi i ON i.numped = c.numped
                                    JOIN pcpromoi p ON p.codprod = i.codprod
                                    JOIN pcpromoc pc ON pc.codpromocao = p.codpromocao
                                WHERE
                                    p.codpromocao = {promotionCode} AND
                                    c.dtcancel IS NULL AND
                                    (
                                        c.data >= pc.dtinicio AND
                                        c.data < pc.dtfim + 1
                                    )
                                GROUP BY 
                                    c.codusur
                """);

            return await query.ToListAsync();
        }

        public async Task<List<TotalRevenueResponse>> CalculateRevenueByMonth(DateTime init, DateTime end)
        {
            var query = _context.Database.SqlQueryRaw<TotalRevenueResponse>($"""
                                SELECT
                                     c.codusur AS SellerId,
                                     SUM(i.qt * i.pvenda) AS Revenue
                                FROM
                                    cf_campanha_rca_score crs
                                    JOIN pcpedc c ON c.codusur = crs.rca_id
                                    JOIN pcpedi i ON i.numped = c.numped
                                    JOIN pcpromoi p ON p.codprod = i.codprod
                                WHERE
                                    p.codpromocao = {init.Year * 100} AND
                                    c.dtcancel IS NULL AND
                                    (
                                        c.data >= DATE '{init:yyyy-MM-dd}' AND
                                        c.data < DATE '{end.AddDays(1):yyyy-MM-dd}'
                                    )
                                GROUP BY 
                                    c.codusur
                """);

            return await query.ToListAsync();
        }

        public async Task<List<OrderDetailResponse>> GetCanceledsByMonth(DateTime initIn, DateTime endIn)
        {
            var query = _context.Database.SqlQuery<OrderDetailResponse>($"""
                            SELECT
                                 pi.qt AS Quantity,
                                 pc.numped AS OrderId,
                                 pi.data AS DateOfSale,
                                 pc.codusur AS SellerId,
                                 pi.codprod AS ProductId,
                                 pi.codcli AS ConsumerId,
                                 pc.dtcancel AS CanceledIn,
                                 c.cliente AS ConsumerName,
                                 p.descricao AS ProductDescription,
                                 pm.qtpontoscliente AS ProductPromotionPoints
                            FROM
                                 cf_campanha_rca_score s
                                 JOIN pcpedc pc ON pc.codusur = s.rca_id
                                 JOIN pcpedi pi ON pi.numped = pc.numped
                                 JOIN pcpromoi pm ON pm.codprod = pi.codprod
                                 JOIN pcclient c ON c.codcli = pc.codcli
                                 JOIN pcprodut p ON p.codprod = pi.codprod
                            WHERE
                                 pc.dtcancel IS NOT NULL AND
                                 pc.data >= DATE {initIn:yyyy-MM-dd} AND
                                 pc.data < DATE {endIn.AddDays(1):yyyy-MM-dd}
                    """);

            return await query.ToListAsync();
        }
    }
}
