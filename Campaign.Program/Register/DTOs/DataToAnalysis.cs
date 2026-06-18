using System.Text.Json.Serialization;

namespace Campaign.Program.Register.DTOs
{
    public record DataToAnalysis([property: JsonPropertyName("sellerId")]
                                 int SellerId,
                                 [property: JsonPropertyName("score")]
                                 int Score,
                                 [property: JsonPropertyName("coupons")]
                                 int Coupons,
                                 [property: JsonPropertyName("revenue")]
                                 decimal Revenue,
                                 [property: JsonPropertyName("lastPointByAccess")]
                                 DateTime LastPointByAccess);
}
