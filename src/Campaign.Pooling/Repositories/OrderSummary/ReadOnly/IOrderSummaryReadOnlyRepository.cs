namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderSummaryReadOnlyRepository
    {
        Task<int[]> GetReactivatedClients(int[] clientsIds, DateTime cutoffDate);
    }
}
