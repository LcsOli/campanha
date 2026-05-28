using Campaign.API.Commands.User.Create;
using Entity = Campaign.Shared.DataBaseContext.Entities.Users;

namespace Campaign.API.Handlers.User.RegisterUser
{
    public interface IRegisterUserHandler
    {
        Task<Entity.User> Handle(InsertUserCommand cmd);
    }
}
