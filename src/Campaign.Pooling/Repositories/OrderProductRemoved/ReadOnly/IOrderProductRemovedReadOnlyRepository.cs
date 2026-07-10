using Campaign.Processor.API.DTO.Response.OrderProductRemoved.Items.Get;

namespace Campaign.Pooling.Repositories.OrderProductRemoved.ReadOnly
{
    public interface IOrderProductRemovedReadOnlyRepository
    {
        Task<List<OrderProductItemsRemoved>> GetByOrdersIds(int promotionCode, int[] ordersIds);
    }
}
