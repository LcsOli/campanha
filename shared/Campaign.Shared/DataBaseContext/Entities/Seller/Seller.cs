namespace Campaign.Shared.DataBaseContext.Entities.Seller
{
    public class Seller
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public int ManagerId { get; private set; }
    }
}
