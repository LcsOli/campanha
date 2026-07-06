using Entity = Campaign.Shared.DataBaseContext.Entities.OrderProductRemoved;

namespace Campaign.Pooling.Repositories.OrderProductRemoved.ReadOnly
{
    public interface IOrderProductRemovedReadOnlyRepository
    {
        Task<List<Entity.OrderProductRemoved>> GetByOrdersIds(int promotionCode, int[] ordersIds);
    }
}
