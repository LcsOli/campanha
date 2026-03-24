using Campaign.API.Commands.User.Auth;
using Campaign.Shared.DTOs.Response.User;

namespace Campaign.API.Handlers.User.AuthOrchestrator
{
    public interface IAuthOrchestratorHandler
    {
        Task<TokenJwtResponse> Handle(AuthOrchestratorCommand cmd);
    }
}
