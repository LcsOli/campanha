using System.Net;
using System.Reflection;
using Campaign.API.Configuration.Exceptions;
using Campaign.API.Configuration.Attributes.Enums;

namespace Campaign.API.Extensions.Enums
{
    public static class EnumAttributeExtension
    {
        public static EnumDisplayDescriptionAttribute? GetEnumsAttributes(this Enum value)
        {
            var type = value.GetType();
            var member = type.GetMember(value.ToString()).FirstOrDefault();

            return member?.GetCustomAttribute<EnumDisplayDescriptionAttribute>();
        }

        public static string GetTranslatedDescription(this Enum value)
        {
            return value.GetEnumsAttributes()?.DescriptionTransalated ?? value.ToString();
        }

        public static int GetNumericValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static E EnumDescriptionTranslatedToNumericValue<E>(this string value) where E : Enum
        {
            if (string.IsNullOrEmpty(value))
                throw new CompaignException(HttpStatusCode.InternalServerError, "Valor da busca pelo enumerador não definida.");

            foreach (var item in Enum.GetValues(typeof(E)))
            {
                if (((E)item).GetTranslatedDescription().Equals(value))
                    return (E)item;
            }

            throw new CompaignException(HttpStatusCode.InternalServerError, $"Não foi possível encontrar o valor {value} no enumerador {typeof(E).Name}.");
        }
    }
}
