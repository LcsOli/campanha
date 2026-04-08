using Campaign.Pooling.DTO.Response.Get;

namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderSummaryReadOnlyRepository
    {
        Task<List<SellersQuantityConsumersResponse>> GetSellersIdsThatReactivatedConsumers(int[] consumersIds, int promotionCode);
        Task<List<SellersQuantityConsumersResponse>> GetSellersIdsThatRegisteredsConsumers(int[] consumersIds, int promotionCode);
    }
}
