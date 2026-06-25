using Campaign.Pooling.DTO.Response.Seller;

namespace Campaign.Processor.API.DTO.Response.Seller
{
    public record RegisteredsConsumerResponse : SellersConsumersResponse
    {
        public RegisteredsConsumerResponse(int SellerId,
                                           int CustomerId,
                                           DateTime RegisteredIn) : base(SellerId, CustomerId, RegisteredIn)
        { }
    }
}
