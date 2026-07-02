using static Campaign.API.DTO.SellerScoreProductSummary.Response.SellerScoreClientSummaryResponse;

namespace Campaign.API.DTO.SellerScoreProductSummary.Response
{
    public record SellerScoreClientSummaryResponse(List<Customer> Customers, Resume Resumes)
    {
        public record Customer(int CustomerId, string Name, string EventType, DateTime RegisteredIn, DateTime? ReactivatedIn);
        public record Reactivated(decimal Score, int TotalClients);
        public record Registered(decimal Score, int TotalClients);
        public record Resume(Reactivated Reactivateds, Registered Registered)
        {
            public decimal TotalScore => Reactivateds.Score + Registered.Score;
        }
    }
}
