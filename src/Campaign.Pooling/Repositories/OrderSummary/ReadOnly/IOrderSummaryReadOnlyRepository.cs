namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderSummaryReadOnlyRepository
    {
        Task<int[]> GetSellersIdsThatReactivatedConsumers(int[] clientsIds,
                                                       int promotionCode,
                                                       DateTime dtWeekToStopProcess,
                                                       DateTime dtWeekToStartProcess);

        Task<int[]> GetSellersIdsThatRegisteredsConsumers(int[] clientsIds,
                                                       int promotionCode,
                                                       DateTime dtWeekToStopProcess,
                                                       DateTime dtWeekToStartProcess);
    }
}
