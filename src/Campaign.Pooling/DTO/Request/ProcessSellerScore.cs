namespace Campaign.Pooling.DTO.Request
{
    public record ProcessSellerScore(int PromotionCode, DateTime DtWeekToStartProcess, DateTime DtWeekToStopProcess);
}