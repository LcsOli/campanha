using Campaign.Shared.DTOs.Response.User;
using Entities = Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.API.Handlers.User.GetUser.Mapper
{
    public static class GetUserMapper
    {
        public static UserDetailsResponse ToResponse(Entities.Users.User entity)
        {
            return new(entity.Id, entity.Name, entity.SellerId, entity.LastAccess);
        }
    }
}
