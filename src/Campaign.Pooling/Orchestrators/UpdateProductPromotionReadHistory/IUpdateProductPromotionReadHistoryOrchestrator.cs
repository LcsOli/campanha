namespace Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory
{
    public interface IUpdateProductPromotionReadHistoryOrchestrator
    {
        Task Execute(int promotionCode);
    }
}
