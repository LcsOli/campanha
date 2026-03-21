using Campaign.API.Enums.Role;

namespace Campaign.API.Configuration.DataBaseContext.Entities.Users
{
    public class User
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; } = default!;
        public string Document { get; protected set; } = default!;
        public string Password { get; protected set; } = default!;
        public Roles Roles { get; protected set; }
        public DateTime? LastAccess { get; protected set; }
        public int? TeamId { get; protected set; }
        public int? ManagerId { get; protected set; }
        public Team? Team { get; protected set; }
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
            Password = password;
            Document = document;
            ManagerId = managerId;
            LastAccess = lastAccess;
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
                Password = password,
                ManagerId = managerId
            };
        }

        public void SetPassword(string password)
        {
            Password = password;
        }
    }
}