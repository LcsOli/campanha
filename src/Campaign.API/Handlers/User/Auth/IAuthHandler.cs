using Campaign.API.Commands.User.Auth;
using Campaign.Shared.DTOs.Response.User;

namespace Campaign.API.Handlers.User.Auth
{
    public interface IAuthHandler
    {
        Task<TokenJwtResponse> Handle(AuthCommand cmd);
    }
}
