using Campaign.Processor.API.DTO.Response.Seller;
using Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.Summaries.Create
{
    public record RegisterReactivatedsCommand : RegisterCommand
    {
        public List<ReactivatedsConsumerResponse> ReactivatedsConsumers { get; }
        public RegisterReactivatedsCommand(int PromotionCode,
                                           List<SellerScore> SellersScores,
                                           List<ReactivatedsConsumerResponse> ReactivatedsConsumers) : base(PromotionCode, SellersScores)
        {
            this.ReactivatedsConsumers = ReactivatedsConsumers;
        }
    }
}
