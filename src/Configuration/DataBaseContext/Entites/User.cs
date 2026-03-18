using Campaign.API.Enums.Role;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace Campaign.API.Configuration.DataBaseContext.Entites
{
    public class User
    {
        [Key]
        public int Id { get; private set; }
        [NotNull]
        [MaxLength(100)]
        public string Name { get; private set; } = default!;
        [NotNull]
        [MaxLength(14)]
        public string Document { get; private set; } = default!;
        [NotNull]
        [MaxLength(10)]
        public string Password { get; private set; } = default!;
        [NotNull]
        public Roles Roles { get; private set; }
        public DateTime? LastAccess { get; private set; }
        public int TeamId { get; private set; }
        public int? ManagerId { get; private set; }
        public Team? Team { get; private set; }
        public User? Manager { get; private set; }

        public User(int id,
                    int teamId,
                    string name,
                    Roles roles,
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
            Password = password;
            ManagerId = managerId;
            LastAccess = lastAccess;
        }

        public void UpdateAccess()
        {
            LastAccess = DateTime.Now;
        }
    }
}
