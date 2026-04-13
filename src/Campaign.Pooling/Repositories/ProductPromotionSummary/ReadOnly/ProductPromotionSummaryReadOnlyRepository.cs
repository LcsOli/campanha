using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Pooling.DTO.Response.ProductPromotionSummary;

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
                         plast.dtfim AS "LastDtEnd"
                     FROM
                         pcpromoc pcurrent
                         JOIN pcpromoc pprevious ON pprevious.codpromocao = pcurrent.codpromocao - 1
                         JOIN pcpromoc plast ON plast.codpromocao = CONCAT(EXTRACT(YEAR FROM pcurrent.dtinicio), '00')
                     WHERE
                         pcurrent.codpromocao = {currentPromotionCode}
            """);

            return await query.FirstOrDefaultAsync();
        }
    }
}
