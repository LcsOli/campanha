using Campaign.API.Commands.User.Create;

namespace Campaign.API.Handlers.User.RegisterUser
{
    public interface IRegisterUserHandler
    {
        Task<int> Handle(RegisterUserCommand cmd);
    }
}
