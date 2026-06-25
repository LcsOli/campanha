using Campaign.Pooling.DTO.Response.Seller;

namespace Campaign.Processor.API.DTO.Response.Seller
{
    public record ReactivatedsConsumerResponse : SellersConsumersResponse
    {
        public ReactivatedsConsumerResponse(int SellerId,
                                            int CustomerId,
                                            DateTime RegisteredIn,
                                            DateTime ReactivatedIn) : base(SellerId, CustomerId, RegisteredIn, ReactivatedIn)
        { }
    }
}
