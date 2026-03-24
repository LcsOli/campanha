using Campaign.API.Commands.User.Get;
using Campaign.Shared.DTOs.Response.User;

namespace Campaign.API.Handlers.User.GetUser
{
    public interface IGetUserHandler
    {
        Task<UserDetailsResponse> Handle(GetUserCommand cmd);
    }
}
