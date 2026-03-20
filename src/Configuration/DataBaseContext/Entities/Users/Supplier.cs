using Campaign.API.Enums.Role;

namespace Campaign.API.Configuration.DataBaseContext.Entities.User.Users
{
    public class Supplier : Entities.Users.User
    {
        public static Entities.Users.User Generate(Roles roles,
                                                   string name,
                                                   string document,
                                                   string password)
        {
            return new Supplier
            {
                Roles = roles,
                Name = name,
                Document = document,
                Password = password
            };
        }
    }
}
