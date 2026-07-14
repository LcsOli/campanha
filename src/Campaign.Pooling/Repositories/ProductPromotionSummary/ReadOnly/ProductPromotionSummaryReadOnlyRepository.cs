using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Pooling.DTO.Response.ProductPromotionSummary;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly
{
    public class ProductPromotionSummaryReadOnlyRepository : IProductPromotionSummaryReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public ProductPromotionSummaryReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<ProductPromotionSummariesDatesResponse?> GetProductPromotionSummariesDates(int currentPromotionCode)
        {
            var query = _context.Database.SqlQuery<ProductPromotionSummariesDatesResponse>($"""
                     SELECT
                         pprevious.dtinicio AS "PreviousDtInit",
                         pprevious.dtfim AS "PreviousDtEnd",
                         pcurrent.dtinicio AS "CurrentDtInit",
                         pcurrent.dtfim AS "CurrentDtEnd",
                         plast.dtfim AS "DtEndOfCampaign"
                     FROM
                         pcpromoc pcurrent
                         JOIN pcpromoc pprevious ON pprevious.codpromocao = pcurrent.codpromocao - 1
                         JOIN pcpromoc plast ON plast.codpromocao = EXTRACT(YEAR FROM pcurrent.dtinicio) * 100
                     WHERE
                         pcurrent.codpromocao = {currentPromotionCode}
            """);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Entity.ProductPromotionSummary?> GetByPromotionCode(int promotionCode)
        {
            return await _context.ProductPromotionSummaries.FirstOrDefaultAsync(p => p.Id == promotionCode);
        }

        public async Task<int[]> GetPromotionsCodesByPeriod(DateTime initIn, DateTime endIn)
        {
            return await _context.ProductPromotionSummaries.Where(x => x.InitIn.Date >= initIn && x.EndIn.Date <= endIn)
                                                           .Select(x => x.Id)
                                                           .ToArrayAsync();
        }
    }
}
