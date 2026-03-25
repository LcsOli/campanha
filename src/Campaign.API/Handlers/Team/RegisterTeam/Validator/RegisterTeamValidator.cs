using FluentValidation;
using Campaign.Shared.FluentValidator;
using Campaign.API.Commands.Team.Create;

namespace Campaign.API.Handlers.Team.RegisterTeam.Validator
{
    public class RegisterTeamValidator : FluentValidator<RegisterTeamCommand>
    {
        public RegisterTeamValidator()
        {
            RuleFor(t => t.Id)
                .Empty()
                .LessThan(0)
                .WithMessage("Identificador do time deve ser definido.");

            RuleFor(t => t.Name)
                .Empty()
                .Null()
                .WithMessage("Nome do time deve ser definido.");

            RuleFor(t => t.Description)
                .Empty()
                .Null()
                .WithMessage("Descrição do time deve ser definido.");
        }
    }
}
