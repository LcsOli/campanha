using Campaign.API.Commands.User.Create;
using Entities = Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Handlers.User.RegisterUser.Mapper
{
    public static class RegisterUserMapper
    {
        public static Entities.User ToEntity(RegisterUserCommand cmd)
        {
            return new(name: cmd.Name,
                       roles: cmd.Role,
                       teamId: cmd.TeamId,
                       document: cmd.Document,
                       password: cmd.Password,
                       managerId: cmd.ManagerId);
        }
    }
}
