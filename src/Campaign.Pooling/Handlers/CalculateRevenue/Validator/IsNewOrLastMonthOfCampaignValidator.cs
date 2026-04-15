using Campaign.Pooling.DTO.Response.ProductPromotionSummary;

namespace Campaign.Pooling.Handlers.CalculateRevenue.Validator
{
    public static class IsNewOrLastMonthOfCampaignValidator
    {
        public static bool Validate(ProductPromotionSummariesDatesResponse? entity)
        {
            if (entity == null)
                return false;

            var isNewMonth = entity!.PreviousDtInit.Month < entity.CurrentDtInit.Month;
            var isLastMonthOfCampaign = entity.CurrentDtEnd >= entity.DtEndOfCampaign;

            return isNewMonth || isLastMonthOfCampaign;
        }
    }
}
