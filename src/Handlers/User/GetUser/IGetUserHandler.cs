using Campaign.API.Commands.User.Get;
using Campaign.API.DTOs.Response.User;

namespace Campaign.API.Handlers.User.GetUser
{
    public interface IGetUserHandler
    {
        Task<UserDetailsResponse> Handle(GetUserCommand cmd);
    }
}
