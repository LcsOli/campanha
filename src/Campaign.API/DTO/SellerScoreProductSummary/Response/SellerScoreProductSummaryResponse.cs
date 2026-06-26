using static Campaign.API.DTO.SellerScoreProductSummary.Response.SellerScoreProductSummaryResponse;

namespace Campaign.API.DTO.SellerScoreProductSummary.Response
{
    public record SellerScoreProductSummaryResponse(List<Product> Products, Resume Resumes)
    {
        public record Product(int Id, string Description, decimal Points, List<Customer> Customers);
        public record Customer(int CustomerId, string CustomerName);

        public record Resume(decimal Points, int QtyCustomers)
        {
            public decimal TotalPoints => Points * QtyCustomers;
        }
    }
}
