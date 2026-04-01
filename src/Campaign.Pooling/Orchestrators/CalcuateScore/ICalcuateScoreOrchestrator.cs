namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public interface ICalcuateScoreOrchestrator
    {
        Task Execute(int promotionCode, DateTime initIn, DateTime endIn);
    }
}
