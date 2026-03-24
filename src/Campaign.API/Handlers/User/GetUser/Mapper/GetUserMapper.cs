using Campaign.API.DTOs.Response.User;
using Entities = Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Handlers.User.GetUser.Mapper
{
    public static class GetUserMapper
    {
        public static UserDetailsResponse ToResponse(Entities.Users.User entity)
        {
            return new(entity.Id, entity.Name, entity.LastAccess);
        }
    }
}
