using System.Net;
using Campaign.API.Commands.User.Get;
using Campaign.API.DTOs.Response.User;
using Campaign.API.Configuration.Exceptions;
using Campaign.API.Repositories.User.ReadOnly;
using Campaign.API.Handlers.User.GetUser.Mapper;

namespace Campaign.API.Handlers.User.GetUser
{
    public class GetUserHandler : IGetUserHandler
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        public GetUserHandler(IUserReadOnlyRepository userReadOnlyRepository)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<UserDetailsResponse> Handle(GetUserCommand cmd)
        {
            if (cmd.Id <= 0)
                throw new CompaignException(HttpStatusCode.BadRequest, "Identificação do usuário deve ser definida.");

            var user = await _userReadOnlyRepository.GetById(cmd.Id) ??
                throw new CompaignException(HttpStatusCode.NotFound, "Usuário não encontrado.");

            return GetUserMapper.ToResponse(user);
        }
    }
}