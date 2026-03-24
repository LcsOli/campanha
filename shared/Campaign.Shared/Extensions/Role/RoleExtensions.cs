using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.Shared.Enums.Role;

namespace Campaign.Shared.Extensions.Role
{
    public static class RoleExtensions
    {
        public static string RoleToDesc(this Roles role) => role switch
        {
            Roles.Supplier => "supplier",
            Roles.Manager => "manager",
            Roles.User => "user",
            _ => throw new CompaignException(HttpStatusCode.InternalServerError, "Erro ao buscar descrição da role.")
        };

        public static string RoleToDescTranslated(this Roles role) => role switch
        {
            Roles.Supplier => "fornecedor",
            Roles.Manager => "gerente",
            Roles.User => "usuario",
            _ => throw new CompaignException(HttpStatusCode.InternalServerError, "Erro ao converter valor numérico em descrição traduzida da role.")
        };

        public static Roles TranslatedRoleDescToRole(this string roleDesc) => roleDesc switch
        {
            "fornecedor" => Roles.Supplier,
            "gerente" => Roles.Manager,
            "usuario" => Roles.User,
            _ => throw new CompaignException(HttpStatusCode.InternalServerError, "Erro ao converter descrição traduzida da role para valor numérico.")
        };
    }
}
