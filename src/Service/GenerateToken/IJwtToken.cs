using Campaign.API.Enums.Role;

namespace Campaign.API.Service.GenerateToken
{
    public interface IJwtToken
    {
        string Generate(string name, Roles role);
    }
}
