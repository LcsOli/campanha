using Campaign.API.Commands.User.Create;
using Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Handlers.RegisterUser.Mapper
{
    public static class RegisterUserMapper
    {
        public static User ToEntity(RegisterUserCommand cmd)
        {
            return new();
        }
    }
}
