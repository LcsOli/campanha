namespace Campaign.Pooling.DTO.Response.ProductPromotionSummary
{
    public record ProductPromotionsSummariesDatesInitAndEnd(DateTime OldProductPromotionInitDate,
                                                            DateTime OldProductPromotionEndDate,
                                                            DateTime NewProductPromotionInitDate,
                                                            DateTime NewProductPromotionEndDate);
}
