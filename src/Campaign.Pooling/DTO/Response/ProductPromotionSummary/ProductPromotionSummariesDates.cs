namespace Campaign.Pooling.DTO.Response.ProductPromotionSummary
{
    public record ProductPromotionSummariesDates(DateTime PreviousPromotionDtInit,
                                                 DateTime PreviousPromotionDtEnd,
                                                 DateTime CurrentPromotionDtInit,
                                                 DateTime CurrentPromotionDtEnd,
                                                 DateTime LastPromotionDtEnd);
}
