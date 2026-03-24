using Campaign.Shared.Enums.Role;

namespace Campaign.API.DTOs.Request.User.Create
{
    public record RegisterUserRequest(Roles Role,
                                      int TeamId,
                                      string Name,
                                      int? ManagerId,
                                      string Document,
                                      string Password);
}
