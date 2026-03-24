using Campaign.Shared.Attributes.Enums;

namespace Campaign.Shared.Enums.Role
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
