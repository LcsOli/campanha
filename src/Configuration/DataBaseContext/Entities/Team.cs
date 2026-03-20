namespace Campaign.API.Configuration.DataBaseContext.Entities
{
    public class Team
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
    }
}
