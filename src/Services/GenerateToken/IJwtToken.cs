using Campaign.API.Enums.Role;

namespace Campaign.API.Services.GenerateToken
{
    public interface IJwtToken
    {
        string Generate(string name, Roles role);
    }
}
