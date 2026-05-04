using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.Shared.Enums.Role;
using Campaign.API.Services.Token;
using Campaign.API.Services.Password;
using Campaign.API.Commands.User.Auth;
using Campaign.Shared.DTOs.Response.User;
using Campaign.API.Repositories.User.ReadOnly;
using Campaign.API.Handlers.User.Auth.Validator;

namespace Campaign.API.Handlers.User.Auth
{
    public class AuthHandler : IAuthHandler
    {
        private readonly IJwtToken _jwtToken;
        private readonly IPasswordService _passwordService;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public AuthHandler(IJwtToken jwtToken,
                           IPasswordService passwordService,
                           IUserReadOnlyRepository userReadOnlyRepository)
        {
            _jwtToken = jwtToken;
            _passwordService = passwordService;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<TokenJwtResponse> Handle(AuthCommand cmd)
        {
            new AuthValidator()
                .Validate(cmd);

            var user = await _userReadOnlyRepository.GetByDocument(cmd.Document) ??
                throw new CompaignException(HttpStatusCode.NotFound, "Usuário não cadastrado.");

            var passwordIsValid = _passwordService.VerifyPassword(user, user.HashedPassword, cmd.Password);

            if (!passwordIsValid)
                throw new CompaignException(HttpStatusCode.Forbidden, "Senha inválida.");

            //TODO - Criar factory para gerar o token de acordo com a role do usuário

            return user.Roles switch
            {
                Roles.User => 
                    new(_jwtToken.Generate(user.Id.ToString(), user.Name, user.TeamId?.ToString()!, user.SellerId?.ToString()!, user.Roles.ToString().ToLower())),
                Roles.Supplier => 
                    new(_jwtToken.Generate(user.Id.ToString(), user.Name, user.SellerId.ToString()!, user.Roles.ToString().ToLower())),
                _ => 
                    new(_jwtToken.Generate(user.Id.ToString(), user.Name, user.Roles.ToString().ToLower()))
            };
        }
    }
}
