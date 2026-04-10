namespace Campaign.Shared.DTOs.Response.User
{
    public record UserDetailsResponse(int Id,
                                      string Name,
                                      int SellerId,
                                      DateTime? LastAccess);
}
