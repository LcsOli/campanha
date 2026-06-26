namespace Campaign.Shared.DataBaseContext.Entities.Customer
{
    public class Customer
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public DateTime RegisteredAt { get; private set; }
    }
}
