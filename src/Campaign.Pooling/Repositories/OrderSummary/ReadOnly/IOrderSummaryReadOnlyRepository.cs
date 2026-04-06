namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderSummaryReadOnlyRepository
    {
        Task<int[]> GetReactivatedClients(int[] clientsIds,
                                          int promotionCode,
                                          DateTime dtWeekToStopProcess,
                                          DateTime dtWeekToStartProcess);
    }
}
