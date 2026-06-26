using Entity = Campaign.Shared.DataBaseContext.Entities.Customer;

namespace Campaign.API.Repositories.Customer.ReadOnly
{
    public interface ICustomerReadOnlyRepository
    {
        Task<List<Entity.Customer>> GetByIds(int[] id);
    }
}
