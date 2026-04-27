using Campaign.Shared.Enums.Role;

namespace Campaign.API.Services.Token
{
    public interface IJwtToken
    {
        string Generate(string userId, string name, string? teamId, string? sellerId, Roles role);
    }
}
