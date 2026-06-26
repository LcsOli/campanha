using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.API.Repositories.Product.ReadOnly
{
    public interface IProductReadOnlyRepository
    {
        Task<List<Entity.Product>> GetByIds(int[] ids);
    }
}
