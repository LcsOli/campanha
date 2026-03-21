using Campaign.API.Commands.User.Auth;
using Campaign.API.DTOs.Response.User.Auth;

namespace Campaign.API.Handlers.User.Auth
{
    public interface IAuthHandler
    {
        Task<TokenJwt> Handle(AuthCommand cmd);
    }
}
