namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderSummaryReadOnlyRepository
    {
        Task<int[]> GetReactivatedClients(int[] clientsIds,
                                          int promotionCode,
                                          DateTime periodEnd,
                                          DateTime initOfYear,
                                          DateTime periodStart,
                                          DateTime campaignEndIn,
                                          DateTime campaignInitIn);
    }
}
