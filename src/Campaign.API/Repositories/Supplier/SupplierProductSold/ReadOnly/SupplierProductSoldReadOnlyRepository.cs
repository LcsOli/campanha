using Microsoft.EntityFrameworkCore;
using Campaign.API.DTO.Supplier.Response;
using Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.API.Repositories.Supplier.SupplierProductSold.ReadOnly
{
    public class SupplierProductSoldReadOnlyRepository : ISupplierProductSoldReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SupplierProductSoldReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<SupplierProductSoldResponse>> Get(int supplierId, DateTime initIn, DateTime endIn)
        {
            var query = _context.Database.SqlQueryRaw<SupplierProductSoldResponse>($"""
                    SELECT
                        p.descricao AS ProductName,
                        COUNT(p.codprod) AS Quantity,
                        SUM(pi.qt * pi.pvenda) AS TotalValue
                    FROM
                        pcpedc pc
                        JOIN pcpedi pi on pi.numped = pc.numped
                        JOIN pcprodut p ON p.codfornec = {supplierId} AND p.codprod = pi.codprod
                        JOIN pcpromoi pmi ON pmi.codprod = p.codprod AND pmi.codpromocao = {initIn.Year * 100}
                    WHERE
                        pc.dtcancel IS NULL AND
                        (
                            pi.data >= DATE '{initIn:yyyy-MM-dd}' AND
                            pi.data <= DATE '{endIn:yyyy-MM-dd}'
                        )
                    GROUP BY
                        p.codprod,
                        p.descricao
                    ORDER BY
                        TotalValue
                    DESC
                """);

            return await query.ToListAsync();
        }
    }
}
