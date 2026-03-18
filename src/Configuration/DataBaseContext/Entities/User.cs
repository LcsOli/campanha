
using Campaign.API.Enums.Role;

namespace Campaign.API.Configuration.DataBaseContext.Entities
{
    public class User : UserDefault
    {
        public int TeamId { get; private set; }
        public int? ManagerId { get; private set; }
        public Team? Team { get; private set; }
        public User? Manager { get; private set; }

        /*
         
          protected UserDefault(int id, string name, string document, string password, Roles roles, DateTime? lastAccess)
        {
            Id = id;
            Name = name;
            Document = document;
            Password = password;
            Roles = roles;
            LastAccess = lastAccess;
        }
         
         */

        public User CreateUser(Roles roles,
                               string name,
                               string document,
                               string password)
        {
            return new();
        }
    }
}