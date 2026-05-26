using System.Text.Json.Serialization;

namespace Campaign.Program.Register.DTOs
{
    public class SellersToRegister
    {
        public List<Seller> Sellers { get; set; } = default!;

    }
    public class Seller
    {
        public int SellerId { get; set; }
        public int TeamId { get; set; }
        public int SellerManagerId { get; set; }
        public string Name { get; set; } = default!;
        public string Document { get; set; } = default!;

        public void SetDocument(string? document)
        {
            Document = document!;
        }
    }
}
