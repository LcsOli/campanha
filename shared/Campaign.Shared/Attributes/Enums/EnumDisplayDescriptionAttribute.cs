namespace Campaign.Shared.Attributes.Enums
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class EnumDisplayDescriptionAttribute : Attribute
    {
        public string DescriptionTransalated { get; private set; } = default!;

        public EnumDisplayDescriptionAttribute(string descriptionTransalated)
        {
            DescriptionTransalated = descriptionTransalated ?? string.Empty;
        }
    }
}
