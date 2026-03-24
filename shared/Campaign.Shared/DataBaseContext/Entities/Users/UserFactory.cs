using Campaign.API.Enums.Role;
using System.Text.RegularExpressions;
using Campaign.API.Configuration.DataBaseContext.Entities.User.Users;

namespace Campaign.API.Configuration.DataBaseContext.Entities.Users
{
    public static class UserFactory
    {
        public static User Factory(Roles role,
                                   int? teamId,
                                   string name,
                                   int? managerId,
                                   string document,
                                   string password)
        {
            document = Regex.Replace(document, @"[^\d]", "");

            if (role == Roles.Supplier)
                return Supplier.Generate(role, name, document, password);
            else if (role == Roles.Manager)
                return Manager.Generate(teamId, role, name, document, password);
            else
                return User.Generate(teamId, role, name, managerId, document, password);
        }
    }
}
