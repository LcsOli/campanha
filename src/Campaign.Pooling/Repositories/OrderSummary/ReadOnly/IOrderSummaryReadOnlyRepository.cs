using Campaign.Pooling.DTO.Response.Get;

namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderSummaryReadOnlyRepository
    {
        Task<List<SellersQuantityConsumersReactivatedsResponse>> GetSellersIdsThatReactivatedConsumers(int[] consumersIds, int promotionCode);
    }
}
