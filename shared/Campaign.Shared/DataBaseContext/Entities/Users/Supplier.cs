using Campaign.Shared.Enums.Role;

namespace Campaign.Shared.DataBaseContext.Entities.Users
{
    public class Supplier : User
    {
        public static User Generate(Roles roles,
                                    string name,
                                    int? sellerId,
                                    string document,
                                    string password)
        {
            return new Supplier
            {
                Name = name,
                Roles = roles,
                SellerId = sellerId,
                Document = document,
                HashedPassword = password
            };
        }
    }
}
