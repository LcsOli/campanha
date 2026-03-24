using Campaign.API.Commands.Team.Create;
using Entities = Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.API.Handlers.Team.RegisterTeam.Mapper
{
    public static class RegisterTeamMapper
    {
        public static Entities.Team.Team ToEntity(RegisterTeamCommand cmd)
        {
            return new(cmd.Id, cmd.Name, cmd.Description);
        }
    }
}
