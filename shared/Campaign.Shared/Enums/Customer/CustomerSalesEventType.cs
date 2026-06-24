using Campaign.Shared.Attributes.Enums;

namespace Campaign.Shared.Enums.SellerScoreConsumerType
{
    public enum CustomerSalesEventType
    {
        [EnumDisplayDescriptionAttribute("POSITIVADO")]
        Registered,
        [EnumDisplayDescriptionAttribute("REATIVADO")]
        Reactivated
    }
}