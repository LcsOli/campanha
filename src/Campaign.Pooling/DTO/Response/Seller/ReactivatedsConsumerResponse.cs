using Campaign.Pooling.DTO.Response.Seller;

namespace Campaign.Processor.API.DTO.Response.Seller
{
    public record ReactivatedsConsumerResponse : SellersConsumersResponse
    {
        public DateTime? ReactivatedIn { get; init; }
        public ReactivatedsConsumerResponse(int SellerId,
                                            int CustomerId,
                                            DateTime RegisteredIn,
                                            DateTime? ReactivatedIn) : base(SellerId, CustomerId, RegisteredIn)
        {
            this.ReactivatedIn = ReactivatedIn;
        }
    }
}
