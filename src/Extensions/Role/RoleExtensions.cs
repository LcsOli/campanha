using System.Net;
using Campaign.API.Enums.Role;
using Campaign.API.Configuration.Exceptions;

namespace Campaign.API.Extensions.Role
{
    public static class RoleExtensions
    {
        public static string RoleDescription(this Roles role) => role switch
        {
            Roles.Director => "Director",
            Roles.Manager => "Manager",
            Roles.User => "User",
            _ => throw new CompaignException(HttpStatusCode.InternalServerError, "Erro ao buscar descrição da role.")
        };
    }
}
