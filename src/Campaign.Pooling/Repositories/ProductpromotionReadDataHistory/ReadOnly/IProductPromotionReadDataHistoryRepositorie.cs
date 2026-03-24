namespace Campaign.Pooling.Repositories.ProductpromotionReadDataHistory.ReadOnly
{
    public interface IProductPromotionReadDataHistoryRepositorie
    {
        Task<DateTime?> GetDateOfMostRecent();
    }
}
