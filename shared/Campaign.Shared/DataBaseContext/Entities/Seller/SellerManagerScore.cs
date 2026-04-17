
namespace Campaign.Shared.DataBaseContext.Entities.Seller
{
    public class SellerManagerScore
    {
        public int Id { get; private set; }
        public int SellerManagerId { get; private set; }
        public SellerManager? SellerManager { get; private set; }
    }
}
