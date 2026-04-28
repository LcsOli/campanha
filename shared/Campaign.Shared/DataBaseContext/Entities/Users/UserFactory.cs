using Campaign.Shared.Enums.Role;
using System.Text.RegularExpressions;

namespace Campaign.Shared.DataBaseContext.Entities.Users
{
    public static partial class UserFactory
    {
        public static User Create(Roles role,
                                   int? teamId,
                                   string name,
                                   int? sellerId,
                                   int? supplierId,
                                   string document,
                                   string password)
        {
            document = formatDocument().Replace(document, "");

            if (role == Roles.Supplier)
                return Supplier.Generate(role, name, sellerId, supplierId, document, password);
            else if (role == Roles.Manager)
                return Manager.Generate(teamId, role, name, sellerId, document, password);
            else
                return User.Generate(teamId, role, name, sellerId, document, password);
        }

        [GeneratedRegex(@"[^\d]")]
        private static partial Regex formatDocument();
    }
}
