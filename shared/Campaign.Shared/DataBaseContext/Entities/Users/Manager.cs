using Campaign.Shared.Enums.Role;

namespace Campaign.Shared.DataBaseContext.Entities.Users
{
    public class Manager : User
    {
        public static User Generate(int? teamId,
                                    Roles roles,
                                    string name,
                                    string document,
                                    string password)
        {
            return new Manager
            {
                Name = name,
                Roles = roles,
                Document = document,
                HashedPassword = password
            };
        }
    }
}
