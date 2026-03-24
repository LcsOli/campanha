using Campaign.Shared.Enums.Role;

namespace Campaign.API.Commands.User.Create
{
    public record RegisterUserCommand(Roles Role,
                                      int? TeamId,
                                      string Name,
                                      int? ManagerId,
                                      string Document,
                                      string Password)
    {
    }
}
