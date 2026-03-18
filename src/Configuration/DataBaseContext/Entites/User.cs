
namespace Campaign.API.Configuration.DataBaseContext.Entites
{
    public class User : UserDefault
    {
        public int TeamId { get; private set; }
        public int? ManagerId { get; private set; } 
        public Team? Team { get; private set; }
        public User? Manager { get; private set; }
    }
}
