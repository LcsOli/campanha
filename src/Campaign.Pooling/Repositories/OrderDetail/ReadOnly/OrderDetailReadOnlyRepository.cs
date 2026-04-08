using Microsoft.EntityFrameworkCore;
using Campaign.Pooling.DTO.Response.Get;
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
                                pi.pvenda as Price,
                                pi.codprod as ProductId,
                                pi.codcli as ConsumerId,
                                pi.qt as Quantity,
                                pi.data as DateOfSale,
                                pm.qtpontoscliente as ProductPromotionPoints
                            FROM
                                cf_campanha_rca_score s
                                JOIN pcpedc pc ON pc.codusur = s.rca_id
                                JOIN pcpedi pi ON pi.numped = pc.numped
                                JOIN pcpromoi pm ON pm.codprod = pi.codprod
                                JOIN pcpromoc pc on pc.codpromocao = pm.codpromocao
                            WHERE
                                pm.codpromocao = {promotionCode} AND
                                (
                                    TO_CHAR(pc.data, 'yyyy-MM-DD') >= TO_CHAR(pc.dtinicio, 'yyyy-MM-DD') AND
                                    TO_CHAR(pc.data, 'yyyy-MM-DD') <= TO_CHAR(pc.dtfim, 'yyyy-MM-DD')
                                )
                    """);
            return await query.ToListAsync();
        }
    }
}
