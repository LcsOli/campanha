using Campaign.API.Commands.User.Create;
using Campaign.Shared.DataBaseContext.Entities.Users;
using Entities = Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.API.Handlers.User.RegisterUser.Mapper
{
    public static class RegisterUserMapper
    {
        public static Entities.Users.User ToEntity(InsertUserCommand cmd)
        {
            return UserFactory.Create(cmd.Role, cmd.TeamId, cmd.Name, cmd.SellerId, cmd.SupplierId, cmd.Document, cmd.Password);
        }
    }
}
