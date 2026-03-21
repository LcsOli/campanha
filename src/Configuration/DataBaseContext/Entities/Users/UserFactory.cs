using Campaign.API.Configuration.DataBaseContext.Entities.User.Users;
using Campaign.API.Enums.Role;
using System.Text.RegularExpressions;

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

            if (role == Roles.User)
                return User.Generate(teamId, role, name, managerId, document, password);
            else if (role == Roles.Supplier)
                return Supplier.Generate(role, name, document, password);
            else 
                return Manager.Generate(teamId, role, name, document, password);
        }
    }
}
