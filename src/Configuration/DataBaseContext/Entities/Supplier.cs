using Campaign.API.Enums.Role;

namespace Campaign.API.Configuration.DataBaseContext.Entities
{
    public class Supplier : UserDefault
    {
        public Supplier(Roles roles,
                        string name,
                        string document,
                        string password) : base(name, document, password, roles) { }
    }
}
