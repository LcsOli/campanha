namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public interface ICalculateScoreOrchestrator
    {
        Task Execute(int promotionCode);
    }
}
