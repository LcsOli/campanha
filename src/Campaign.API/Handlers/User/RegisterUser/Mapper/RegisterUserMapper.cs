using Campaign.API.Commands.User.Create;
using Campaign.API.Configuration.DataBaseContext.Entities.Users;
using Entities = Campaign.API.Configuration.DataBaseContext.Entities.Users;

namespace Campaign.API.Handlers.User.RegisterUser.Mapper
{
    public static class RegisterUserMapper
    {
        public static Entities.User ToEntity(RegisterUserCommand cmd)
        {
            return UserFactory.Factory(cmd.Role, cmd.TeamId, cmd.Name, cmd.ManagerId, cmd.Document, cmd.Password);
        }
    }
}
