using Campaign.Pooling.DTO.Response.ProductPromotionSummary;

namespace Campaign.Pooling.Handlers.CalculateRevenue.Validator
{
    public static class IsEndMonthOrCampaingValidator
    {
        public static bool Validate(ProductPromotionSummariesDatesResponse? entity)
        {
            if (entity == null)
                return false;

            if (entity.CurrentDtInit.Month < entity.CurrentDtEnd.Month)
                return true;

            if(entity.CurrentDtEnd.Day == DateTime.DaysInMonth(entity.CurrentDtInit.Year, entity.CurrentDtInit.Month))
                return true;

            if (entity.CurrentDtEnd >= entity.DtEndOfCampaign)
                return true;

            return false;
        }
    }
}
