using Campaign.API.Commands.User.Auth;
using Campaign.Shared.DTOs.Response.User;

namespace Campaign.API.Orchestrators.Auth
{
    public interface IAuthOrchestrator
    {
        Task<TokenJwtResponse> Execute(AuthOrchestratorCommand cmd);
    }
}
