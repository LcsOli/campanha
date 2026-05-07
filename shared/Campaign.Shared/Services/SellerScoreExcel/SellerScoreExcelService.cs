using ClosedXML.Excel;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Shared.Services.SellerScoreExcel
{
    public class SellerScoreExcelService
    {
        public SellerScoreExcelService()
        {

        }

        public void Create(List<Entity.SellerScore> sellersScore)
        {
            var path = SellerScoreExcelConfigurations.GetPath();

            using var workbook = new XLWorkbook();


        }
    }
}
