using Campaign.Pooling.DTO.Response.ProductPromotionSummary;

namespace Campaign.Pooling.Handlers.CalculateRevenue.Validator
{
    public static class IsNewOrLastMonthOfCampaignValidator
    {
        // TODO - Pensar em uma lógica para que o processamento do faturamento do mês aconteça
        // quando a data final dos produtos atuais seja maior do que a data inicial do mes anterior.

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
