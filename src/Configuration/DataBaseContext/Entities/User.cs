
using Campaign.API.Enums.Role;

namespace Campaign.API.Configuration.DataBaseContext.Entities
{
    public class User : UserDefault
    {
        public int TeamId { get; private set; }
        public int? ManagerId { get; private set; }
        public Team? Team { get; private set; }
        public User? Manager { get; private set; }

        public User(int teamId,
                    Roles roles,
                    string name,
                    int? managerId,
                    string document,
                    string password) : base(name, document, password, roles)
        {
            TeamId = teamId;
            ManagerId = managerId;
        }
    }
}