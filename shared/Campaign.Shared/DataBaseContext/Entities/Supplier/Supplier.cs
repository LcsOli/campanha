
namespace Campaign.Shared.DataBaseContext.Entities.Supplier
{
    public class Supplier
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Document {  get; private set; } = default!;

        public Supplier(string name, string document)
        {
            Name = name;
            Document = document;
        }
    }
}
