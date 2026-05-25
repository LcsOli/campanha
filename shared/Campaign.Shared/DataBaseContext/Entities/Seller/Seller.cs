namespace Campaign.Shared.DataBaseContext.Entities.Seller
{
    public class Seller
    {
        public int Id { get; private set; }
        public int ManagerId { get; private set; }
        public string Name { get; private set; } = default!;
        public char SellerType { get; private set; }
        public string Document { get; private set; } = default!;

        public Seller(int id,
                      string name,
                      int managerId)
        {
            Id = id;
            Name = name;
            ManagerId = managerId;
        }
    }
}
