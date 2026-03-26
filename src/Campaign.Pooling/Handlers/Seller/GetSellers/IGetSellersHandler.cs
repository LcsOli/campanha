using Campaign.Pooling.Commands.Seller.Get;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;


namespace Campaign.Pooling.Handlers.Seller.GetSellers
{
    public interface IGetSellersHandler
    {
        Task<List<Entity.Seller>> Handle(GetSellersCommand cmd);
    }
}
