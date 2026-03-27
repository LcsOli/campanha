namespace Campaign.Pooling.Orchestrators.MainOrchestrator
{
    public interface IMainOrchestrator
    {
        Task Execute(int promotionCode, DateTime initIn);
    }
}
