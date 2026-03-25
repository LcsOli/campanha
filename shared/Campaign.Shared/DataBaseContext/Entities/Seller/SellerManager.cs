namespace Campaign.Shared.DataBaseContext.Entities.Seller
{
    public class SellerManager
    {
        public int Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; } = default!;

        public SellerManager(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
