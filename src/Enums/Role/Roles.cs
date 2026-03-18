using Campaign.API.Configuration.Attributes.Enums;

namespace Campaign.API.Enums.Role
{
    public enum Roles
    {
        [EnumDisplayDescriptionAttribute("gerente")]
        Manager,
        [EnumDisplayDescriptionAttribute("fornecedor")]
        Supplier,
        [EnumDisplayDescriptionAttribute("usuario")]
        User
    }
}
