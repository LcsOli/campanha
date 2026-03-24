using Campaign.API.Commands.Team.Create;

namespace Campaign.API.Handlers.Team.RegisterTeam
{
    public interface IRegisterTeamHandler
    {
        Task Handle(RegisterTeamCommand cmd);
    }
}
