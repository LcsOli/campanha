namespace Campaign.Pooling.DTO.Response.ProductPromotionSummary
{
    public record ProductPromotionSummariesDatesResponse(DateTime PreviousDtInit,
                                                         DateTime PreviousDtEnd,
                                                         DateTime CurrentDtInit,
                                                         DateTime CurrentDtEnd,
                                                         DateTime LastDtEnd);
}
