using Campaign.API.Commands.User.Update;

namespace Campaign.API.Handlers.User.UpdateLastAccess
{
    public interface IUserUpdateLastAccessHandler
    {
        Task Handle(UserUpdateLastAccessCommand cmd);
    }
}
