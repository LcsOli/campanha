using Campaign.Shared.Enums.Role;

namespace Campaign.Shared.DataBaseContext.Entities.Users
{
    public class Supplier : User
    {
        public static User Generate(Roles roles,
                                    string name,
                                    string document,
                                    string password)
        {
            return new Supplier
            {
                Roles = roles,
                Name = name,
                Document = document,
                HashedPassword = password
            };
        }
    }
}
