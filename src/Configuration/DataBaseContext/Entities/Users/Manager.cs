using Campaign.API.Enums.Role;

namespace Campaign.API.Configuration.DataBaseContext.Entities.Users
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
                Password = password
            };
        }
    }
}
