using Campaign.API.Commands.SellerScore.Create;
using Entity = Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.API.Handlers.SellerScore.RegisterSellerScore.Mapper
{
    public class RegisterSellerScoreMapper
    {
        public static Entity.Seller.SellerScore ToEntity(RegisterSellerScoreCommand cmd)
        {
            return new(cmd.TeamId, cmd.Name, cmd.SellerId, cmd.SellerManagerName, cmd.SellerManagerId);
        }
    }
}
