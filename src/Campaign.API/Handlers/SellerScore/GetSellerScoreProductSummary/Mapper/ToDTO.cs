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

            var productsToResponse = param3.Model.GroupBy(x => new { x.ProductId, x.Points })
                .Select(x =>
                {
                    var productDescription = param1.Model.SingleOrDefault(p => p.Id == x.Key.ProductId);

                    var customersIds = x.Where(p => p.ProductId == x.Key.ProductId).Select(c => c.CustomerId);

                    var customers = param2.Model.Where(c => customersIds.Contains(c.Id))
                                                .Select(c => new SellerScoreProductSummaryResponse.Customer(c.Id, c.Name))
                                                .ToList();

                    return new SellerScoreProductSummaryResponse.Product(x.Key.ProductId,
                                                                         productDescription!.Description,
                                                                         x.Key.Points,
                                                                         customers);
                });

            var qtyCustomes = customers.Count;
            var totalPoints = summaries.Sum(x => x.Points) * qtyCustomes;

            return new([.. productsToResponse]);
        }
    }
}
