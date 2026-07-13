using Campaign.Shared.Mappers;
using Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Shared.DataBaseContext.Entities.Customer;
using Campaign.API.DTO.SellerScoreProductSummary.Response;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.API.Handlers.SellerScore.GetSellerScoreProductSummary.Mapper
{
    public class ToDTO : Mapper<SellerScoreProductSummaryResponse, List<Product>,
                                                                   List<Customer>,
                                                                   List<SellerScoreProductsSummary>>
    {
        public override SellerScoreProductSummaryResponse Parse(MapperParam<List<Product>> param1,
                                                                MapperParam<List<Customer>> param2,
                                                                MapperParam<List<SellerScoreProductsSummary>> param3)
        {
            var products = param1.Model;
            var customers = param2.Model;
            var summaries = param3.Model;

            var productsToResponse = summaries.GroupBy(x => new { x.ProductId, x.Score })
                .Select(x =>
                {
                    var productDescription = products.SingleOrDefault(p => p.Id == x.Key.ProductId);

                    var customersIds = x.Where(p => p.ProductId == x.Key.ProductId).Select(c => c.CustomerId);

                    var customersInfos = customers.Where(c => customersIds.Contains(c.Id))
                                                .Select(c => new SellerScoreProductSummaryResponse.Customer(c.Id, c.Name))
                                                .ToList();

                    return new SellerScoreProductSummaryResponse.Product(Score: x.Key.Score,
                                                                         Id: x.Key.ProductId,
                                                                         Customers: customersInfos,
                                                                         Description: productDescription!.Description);
                });

            var qtyCustomes = customers.Count;
            var totalPoints = summaries.Sum(x => x.Score) * qtyCustomes;

            return new([.. productsToResponse]);
        }
    }
}
