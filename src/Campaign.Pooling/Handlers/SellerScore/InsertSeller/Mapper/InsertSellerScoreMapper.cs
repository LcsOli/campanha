using Campaign.Pooling.Commands.Seller.Create;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.Seller.InsertSeller.Mapper
{
    public static class InsertSellerScoreMapper
    {
        public static List<Entity.SellerScore> ToEntities(List<SellersScoreToCreateCommand> cmds)
        {
            return [.. cmds.Select(ToEntity)];
        }

        public static Entity.SellerScore ToEntity(SellersScoreToCreateCommand cmd)
        {
            return new(cmd.Name, cmd.SellerId, cmd.ManagerName);
        }
    }
}
