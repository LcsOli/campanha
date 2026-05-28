using Campaign.API.Commands.User.Create;

namespace Campaign.API.Orchestrators.RegisterUser
{
    public interface IRegisterUserOrchestrator
    {
        Task<int> Execute(RegisterUserCommand cmd);
    }
}
