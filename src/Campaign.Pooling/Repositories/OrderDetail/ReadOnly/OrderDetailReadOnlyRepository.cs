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

        public async Task<List<OrderDetailResponse>> GetByPromotionCodeAndDateInitAndEnd(int promotionCode)
        {
            var query = _context.Database.SqlQuery<OrderDetailResponse>($"""
                            SELECT
                                pc.codusur as SellerId,
                                pi.codprod as ProductId,
                                pi.codcli as ConsumerId,
                                pi.qt as Quantity,
                                pi.data as DateOfSale,
                                pm.qtpontoscliente as ProductPromotionPoints,
                                pc.numped as OrderId
                            FROM
                                cf_campanha_rca_score s
                                JOIN pcpedc pc ON pc.codusur = s.rca_id
                                JOIN pcpedi pi ON pi.numped = pc.numped
                                JOIN pcpromoi pm ON pm.codprod = pi.codprod
                                JOIN pcpromoc pmc on pmc.codpromocao = pm.codpromocao
                            WHERE
                                pc.dtcancel IS NULL AND
                                pm.codpromocao = {promotionCode} AND
                                (
                                    TO_CHAR(pc.data, 'yyyy-MM-DD') >= TO_CHAR(pmc.dtinicio, 'yyyy-MM-DD') AND
                                    TO_CHAR(pc.data, 'yyyy-MM-DD') <= TO_CHAR(pmc.dtfim, 'yyyy-MM-DD')
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
                                    JOIN pcpedc c on c.codusur = crs.rca_id
                                    JOIN pcusuari u on c.codusur = u.codusur
                                    JOIN pcpedi i on i.numped = c.numped
                                    JOIN pcpromoi p on p.codprod = i.codprod
                                    JOIN pcpromoc pc on pc.codpromocao = p.codpromocao
                                    JOIN pcpromoc pcgeral on pcgeral.codpromocao = to_number(concat(to_char(pc.dtinicio, 'yyyy'), '00'))
                                WHERE
                                    c.dtcancel is null AND
                                    pc.codpromocao = {promotionCode} AND
                                    u.tipovend = 'R' AND
                                    (
                                        TO_CHAR(c.data, 'yyyy-MM-DD') >= TO_CHAR(pcgeral.dtinicio, 'yyyy-MM-DD') AND
                                        TO_CHAR(c.data, 'yyyy-MM-DD') <= TO_CHAR(pc.dtfim, 'yyyy-MM-DD')
                                    )
                                GROUP BY 
                                    c.codusur
                """);

            return await query.ToListAsync();
        }

        public async Task<List<TotalRevenueResponse>> CalculateRevenueByMonth(DateTime init, DateTime end)
        {
            init = init.Date;
            end = end.Date;

            var query = _context.Database.SqlQuery<TotalRevenueResponse>($"""
                                SELECT
                                     c.codusur AS SellerId,
                                     sum(i.qt * i.pvenda) AS Revenue
                                FROM
                                    cf_campanha_rca_score crs
                                    JOIN pcpedc c on c.codusur = crs.rca_id
                                    JOIN pcpedi i on i.numped = c.numped
                                    JOIN pcpromoi p on p.codprod = i.codprod
                                WHERE
                                    p.codpromocao = CONCAT({init.Year}, '00') AND
                                    c.dtcancel is null AND
                                    (
                                        TO_CHAR(c.data, 'yyyy-MM-DD') >= {init.ToString("yyyy-MM-dd")} AND
                                        TO_CHAR(c.data, 'yyyy-MM-DD') <= {end.ToString("yyyy-MM-DD")}
                                    )
                                GROUP BY 
                                    c.codusur
                """);
            
            return await query.ToListAsync();
        }
    }
}
