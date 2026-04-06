namespace Campaign.Shared.DataBaseContext.Entities.Product
{
    public class ProductPromotionSummary
    {
        public int Id { get; private set; }
        public DateTime InitIn { get; private set; }
        public DateTime EndIn { get; private set; }
        public string Description { get; private set; } = default!;
    }
}
