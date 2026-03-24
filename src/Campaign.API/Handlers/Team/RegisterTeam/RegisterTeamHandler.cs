using Campaign.API.Commands.Team.Create;
using Campaign.API.Repositories.Team.WriteOnly;
using Campaign.API.Handlers.Team.RegisterTeam.Mapper;
using Campaign.API.Handlers.Team.RegisterTeam.Validator;

namespace Campaign.API.Handlers.Team.RegisterTeam
{
    public class RegisterTeamHandler : IRegisterTeamHandler
    {
        private readonly ITeamWriteOnlyRepository _teamWriteOnlyRepository;
        public RegisterTeamHandler(ITeamWriteOnlyRepository teamWriteOnlyRepository)
        {
            _teamWriteOnlyRepository = teamWriteOnlyRepository;
        }

        public async Task Handle(RegisterTeamCommand cmd)
        {
            new RegisterTeamValidator()
                .Validate(cmd);

            var team = RegisterTeamMapper.ToEntity(cmd);

            await _teamWriteOnlyRepository.Add(team);
        }
    }
}
