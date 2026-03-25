using Campaign.Shared.Enums.Role;

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
        public int? ManagerId { get; protected set; }
        public Team.Team? Team { get; protected set; }
        public User? Manager { get; protected set; }

        public User() { }

        public User(int id,
                    Roles roles,
                    int? teamId,
                    string name,
                    int? managerId,
                    string document,
                    string password,
                    DateTime? lastAccess)
        {
            Id = id;
            Name = name;
            Roles = roles;
            TeamId = teamId;
            Document = document;
            ManagerId = managerId;
            LastAccess = lastAccess;
            HashedPassword = password;
        }

        public static User Generate(int? teamId,
                                    Roles roles,
                                    string name,
                                    int? managerId,
                                    string document,
                                    string password)
        {
            return new User
            {
                Name = name,
                Roles = roles,
                TeamId = teamId,
                Document = document,
                ManagerId = managerId,
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