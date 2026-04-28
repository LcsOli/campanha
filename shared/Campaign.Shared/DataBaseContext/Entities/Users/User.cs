using Campaign.Shared.Enums.Role;
using SellerEntity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Shared.DataBaseContext.Entities.Users
{
    public class User
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; } = default!;
        public string Document { get; protected set; } = default!;
        public string HashedPassword { get; protected set; } = default!;
        public Roles Roles { get; protected set; }
        public DateTime? LastAccess { get; protected set; }
        public int? TeamId { get; protected set; }
        public int? SellerId { get; protected set; }
        public int? SupplierId { get; protected set; }
        public Team.Team? Team { get; protected set; }
        public SellerEntity.Seller? Seller { get; protected set; }

        public User() { }

        public static User Generate(int? teamId,
                                    Roles roles,
                                    string name,
                                    int? sellerId,
                                    string document,
                                    string password)
        {
            return new User
            {
                Name = name,
                Roles = roles,
                TeamId = teamId,
                SellerId = sellerId,
                Document = document,
                HashedPassword = password
            };
        }

        public void SetPassword(string password)
        {
            HashedPassword = password;
        }

        public void UpdateLastAccess()
        {
            LastAccess = DateTime.Now;
        }
    }
}