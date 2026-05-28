using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.ProductPromotion.WriteOnly
{
    public interface ISellerScoreWriteOnlyRepository
    {
        void Update(Entity.SellerScore entity);
        Task Add(Entity.SellerScore entity);
    }
}
