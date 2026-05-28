using Campaign.Shared.Enums.Role;

namespace Campaign.API.DTOs.Request.User.Create
{
    public record RegisterUserRequest(Roles Role,
                                      int TeamId,
                                      string Name,
                                      int? SellerId,
                                      int? SupplierId,
                                      string Document,
                                      string Password,
                                      int SellerManagerId,
                                      string SellerManagerName);
}
