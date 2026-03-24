namespace Campaign.Shared.DataBaseContext.Entities
{
    public class Team
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        public Team(int id, 
                    string name, 
                    string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
