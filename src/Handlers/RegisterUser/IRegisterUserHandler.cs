using Campaign.API.Commands.User.Create;

namespace Campaign.API.Handlers.RegisterUser
{
    public interface IRegisterUserHandler
    {
        Task Handle(RegisterUserCommand cmd);
    }
}
