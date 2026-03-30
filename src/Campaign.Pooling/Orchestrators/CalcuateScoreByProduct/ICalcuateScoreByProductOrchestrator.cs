namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public interface ICalcuateScoreByProductOrchestrator
    {
        Task Execute(int promotionCode, DateTime initIn, DateTime endIn);
    }
}
