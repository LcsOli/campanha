using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Processor.API.DTO.Response.SellerScoreProductSummary;
using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Processor.API.Repositories.SellerScoreProductsSummary.ReadOnly
{
    public class SellerScoreProductSummaryReadOnlyRepository : ISellerScoreProductSummaryReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerScoreProductSummaryReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.SellerScoreProductsSummary>> GetByIds(int promotionCode, long[] OrdersIds, int[] productsIds)
        {
            var query = from s in _context.SellerScoreProductsSummaries
                        join r in _context.OrderProductRemoveds on new { s.ProductId, s.CustomerId, s.SellerId } equals new { r.ProductId, r.CustomerId, r.SellerId }
                        where
                            OrdersIds.Contains(r.OrderId) &&
                            s.PromotionCode == promotionCode &&
                            productsIds.Contains(s.ProductId) 
                        select s;

            return await query.ToListAsync();
        }


        public async Task<List<Entity.SellerScoreProductsSummary>> GetByPromotionCode(int promotionCode)
        {
            return await _context.SellerScoreProductsSummaries.Where(x => x.PromotionCode == promotionCode).ToListAsync();
        }

        public async Task<List<SellerScoreProductsSummaryOrderId>> Get(int promotionCode)
        {
            return await _context.Database.SqlQuery<SellerScoreProductsSummaryOrderId>($"""
                            select
                               id,
                               numped as orderId
                            from
                            (
                                select 
                                    sp.id,
                                    p.codcli,
                                    sp.cod_produto,
                                    'N' as removido,
                                    min( case 
                                            when 
                                                p.dtcancel is null or
                                                p.dtcancel > pc.dtfim
                                            then
                                                p.numped
                                            end
                                        ) as numped
                                from 
                                     cf_campanha_resumo_rca_score_produto sp 
                                     join pcpedi pi on pi.codprod = sp.cod_produto and pi.codcli = sp.cod_cliente and pi.codusur = sp.rca_id
                                     join pcpedc p on p.numped = pi.numped
                                     join pcpromoc pc on pc.codpromocao = sp.cod_promocao
                                where 
                                    sp.cod_promocao = {promotionCode} and
                                    trunc(p.data) >= trunc(pc.dtinicio) and trunc(p.data) <= trunc(pc.dtfim)
                                group by
                                    sp.id,
                                    p.codcli,    
                                    sp.cod_produto
                            union
                                select
                                    sp.id,
                                    p.codcli,
                                    sp.cod_produto,
                                    'S' as removido,
                                    min( case 
                                            when 
                                                p.dtcancel is null or
                                                p.dtcancel > pc.dtfim
                                            then
                                                p.numped
                                            end
                                        ) as numped
                                from
                                    cf_campanha_resumo_rca_score_produto sp 
                                    join pccortei c on c.codprod = sp.cod_produto and c.codcli = sp.cod_cliente and c.codusur = sp.rca_id
                                    join pcpedc p on p.numped = c.numped
                                    join pcpromoc pc on pc.codpromocao = sp.cod_promocao
                                where
                                    c.qtseparada = 0 and
                                    sp.cod_promocao = {promotionCode} and
                                    trunc(p.data) >= trunc(pc.dtinicio) and trunc(p.data) <= trunc(pc.dtfim)
                                group by
                                    sp.id,
                                    p.codcli,
                                    sp.cod_produto
                            )
                """).ToListAsync();
        }


        public async Task<List<Entity.SellerScoreProductsSummary>> GetByIds(int[] ids)
        {
            return await _context.SellerScoreProductsSummaries.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
