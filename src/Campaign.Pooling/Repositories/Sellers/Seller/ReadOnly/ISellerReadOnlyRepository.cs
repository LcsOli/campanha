namespace Campaign.Pooling.Repositories.Sellers.Seller.ReadOnly
{
    public interface ISellerReadOnlyRepository
    {
        Task GetSellersByIds(int[] sellersIds);
    }
}
