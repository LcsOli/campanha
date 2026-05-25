namespace Campaign.Program.Register.DTOs
{
    public class SellersToRegister
    {
        public List<Seller> Sellers { get; private set; } = default!;

    }
    public class Seller
    {
        public int SellerId { get; private set; }
        public int TeamId { get; private set; }
        public string Document { get; private set; }
        public int SellerManagerId { get; private set; }
        public string Name { get; private set; } = default!;

        public Seller(int sellerId, int sellerManagerId, int teamId, string document, string name)
        {
            Name = name;
            TeamId = teamId;
            SellerId = sellerId;
            Document = document;
            SellerManagerId = sellerManagerId;
        }

        public void SetDocument(string? document)
        {
            Document = document!;
        }
    }
}
