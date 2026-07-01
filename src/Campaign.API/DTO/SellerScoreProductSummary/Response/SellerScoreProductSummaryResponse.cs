using System.Text.Json.Serialization;
using static Campaign.API.DTO.SellerScoreProductSummary.Response.SellerScoreProductSummaryResponse;

namespace Campaign.API.DTO.SellerScoreProductSummary.Response
{
    public record SellerScoreProductSummaryResponse(List<Product> Products)
    {
        public record Product([property: JsonPropertyName("productId")]
                               int Id,
                               string Description,
                               decimal Points,
                               List<Customer> Customers);
        public record Customer(int CustomerId, string CustomerName);
        public record Resume(decimal Scores, int QtyCustomers)
        {
            public decimal TotalPoints => Scores * QtyCustomers;
        }
    }
}
