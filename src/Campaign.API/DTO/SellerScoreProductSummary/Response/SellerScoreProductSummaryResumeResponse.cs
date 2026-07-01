namespace Campaign.API.DTO.SellerScoreProductSummary.Response
{
    public record SellerScoreProductSummaryResumeResponse(decimal Score, int QtyCustomers)
    {
        public decimal TotalScore => Score * QtyCustomers;
    }
}
