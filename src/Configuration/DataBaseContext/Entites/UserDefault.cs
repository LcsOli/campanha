using Campaign.API.Enums.Role;

namespace Campaign.API.Configuration.DataBaseContext.Entites
{
    public abstract class UserDefault
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Document { get; private set; } = default!;
        public string Password { get; private set; } = default!;
        public Roles Roles { get; private set; }
        public DateTime? LastAccess { get; private set; }
        public void UpdateAccess()
        {
            LastAccess = DateTime.Now;
        }
    }
}
