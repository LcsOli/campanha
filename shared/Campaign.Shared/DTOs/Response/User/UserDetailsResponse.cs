namespace Campaign.Shared.DTOs.Response.User
{
    public record UserDetailsResponse(int Id,
                                      string Name,
                                      DateTime? LastAccess);
}
