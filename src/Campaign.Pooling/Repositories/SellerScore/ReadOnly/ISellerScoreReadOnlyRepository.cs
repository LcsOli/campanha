namespace Campaign.Pooling.Repositories.SellerScore.ReadOnly
{
    public interface ISellerScoreReadOnlyRepository
    {
        Task<List<int>> GetSellersInserteds(int[] sellersIds);
    }
}
