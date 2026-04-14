using Campaign.Pooling.DTO.Response.Seller;

namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderSummaryReadOnlyRepository
    {
        Task<List<SellersQuantityConsumersResponse>> GetSellersIdsThatReactivatedConsumers(int promotionCode);
        Task<List<SellersQuantityConsumersResponse>> GetSellersIdsThatRegisteredsConsumers(int promotionCode);
    }
}
