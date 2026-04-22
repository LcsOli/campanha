using Campaign.API.DTO.Team.Response;
using Campaign.API.Repositories.Team.ReadOnly;

namespace Campaign.API.Handlers.Team.Get
{
    public class GetTeamHandle : IGetTeamHandle
    {
        private readonly ITeamReadOnlyRepository _teamReadOnlyRepository;
        public GetTeamHandle(ITeamReadOnlyRepository teamReadOnlyRepository)
        {
            _teamReadOnlyRepository = teamReadOnlyRepository;
        }

        public async Task<List<TeamResponse>> Handle()
        {
            return [.. (await _teamReadOnlyRepository.GetAll()).Select(t => new TeamResponse(t.Id, t.Name, t.Description))];
        }
    }
}
