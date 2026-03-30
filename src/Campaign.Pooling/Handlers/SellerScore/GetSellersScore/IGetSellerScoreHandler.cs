using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.SellerScore.GetSellersScore
{
    public interface IGetSellerScoreHandler
    {
        Task<List<Entity.SellerScore>> Handle();
    }
}
