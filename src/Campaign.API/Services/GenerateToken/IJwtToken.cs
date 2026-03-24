using Campaign.Shared.Enums.Role;

namespace Campaign.API.Services.GenerateToken
{
    public interface IJwtToken
    {
        string Generate(string userId, string name, string? teamId, Roles role);
    }
}
