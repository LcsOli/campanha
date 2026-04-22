using Campaign.API.DTO.Team.Response;

namespace Campaign.API.Handlers.Team.Get
{
    public interface IGetTeamHandle
    {
        Task<List<TeamResponse>> Handle();
    }
}
