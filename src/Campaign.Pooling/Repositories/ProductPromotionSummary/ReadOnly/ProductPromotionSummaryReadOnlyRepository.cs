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

        public async Task<ProductPromotionSummariesDates?> GetProductPromotionSummariesDates(int currentPromotionCode)
        {
            var query = _context.Database.SqlQuery<ProductPromotionSummariesDates>($"""
                                            SELECT
                                                pprevious.dtinicio AS "PreviousPromotionDtInit",
                                                pprevious.dtfim AS "PreviousPromotionDtEnd",
                                                pcurrent.dtinicio AS "CurrentPromotionDtInit",
                                                pcurrent.dtfim AS "CurrentPromotionDtEnd",
                                                plast.dtfim AS "LastPromotionDtEnd"
                                            FROM
                                                pcpromoc pcurrent
                                                JOIN pcpromoc pprevious ON pprevious.codpromocao = pcurrent.codpromocao - 1
                                                JOIN pcpromoc plast ON plast.codpromocao = CONCAT(EXTRACT(YEAR FROM pcurrent.dtinicio), '00')
                                            WHERE
                                                pcurrent.codpromocao = {currentPromotionCode}
            """);

            return await query.FirstOrDefaultAsync();


            /*
            var productPromotionsSummaries = await _context.ProductPromotionSummaries.Where(p => p.Id == previousPromotionCode || p.Id == currentPromotionCode)
                                                                                     .ToListAsync();

            var previousProductPromotionSummary = productPromotionsSummaries.FirstOrDefault(p => p.Id == previousPromotionCode);
            var currentProductPromotionSummary = productPromotionsSummaries.FirstOrDefault(p => p.Id == currentPromotionCode);

            var firstProductPromotionCode = int.Parse(currentProductPromotionSummary!.InitIn.Year.ToString().PadRight(2, '0'));

            var FirstPromotionsSummarie = await _context.ProductPromotionSummaries.FirstOrDefaultAsync(p => p.Id == firstProductPromotionCode);

            return new ProductPromotionsSummariesDatesInitAndEnd(new ProductPromotionsDates(previousProductPromotionSummary!.InitIn, previousProductPromotionSummary.EndIn),
                                                                 new ProductPromotionsDates(currentProductPromotionSummary!.InitIn, currentProductPromotionSummary.EndIn),
                                                                 new ProductPromotionsDates(FirstPromotionsSummarie!.InitIn, FirstPromotionsSummarie.EndIn));
            */
        }
    }
}
