using Campaign.Pooling.DTO.Response.Seller;
using Campaign.Processor.API.DTO.Response.Seller;

namespace Campaign.Pooling.Repositories.OrderSummary.ReadOnly
{
    public interface IOrderSummaryReadOnlyRepository
    {
        Task<List<ReactivatedsConsumerResponse>> GetCustomersReactivatedsBySelller(int promotionCode);
        Task<List<RegisteredsConsumerResponse>> GetCustomersRegisteredsBySelller(int promotionCode);
    }
}
