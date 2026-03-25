using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.Sellers.Seller.ReadOnly
{
    public interface ISellerReadOnlyRepository
    {
        Task<List<Entity.Seller>> GetSellersByIds(int[] sellersIds);
    }
}
