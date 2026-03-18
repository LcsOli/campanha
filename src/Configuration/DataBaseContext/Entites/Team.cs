using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace Campaign.API.Configuration.DataBaseContext.Entites
{
    public class Team
    {
        [Key]
        public int Id { get; private set; }
        [NotNull]
        public string Description { get; private set; } = default!;
    }
}
