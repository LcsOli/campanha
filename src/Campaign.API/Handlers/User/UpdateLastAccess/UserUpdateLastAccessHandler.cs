using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.API.Commands.User.Update;
using Campaign.API.Repositories.User.ReadOnly;
using Campaign.API.Repositories.User.WriteOnly;

namespace Campaign.API.Handlers.User.UpdateLastAccess
{
    public class UserUpdateLastAccessHandler : IUserUpdateLastAccessHandler
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        public UserUpdateLastAccessHandler(IUserReadOnlyRepository userReadOnlyRepository,
                                           IUserWriteOnlyRepository userWriteOnlyRepository)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
        }

        public async Task Handle(UserUpdateLastAccessCommand cmd)
        {
            var user = await _userReadOnlyRepository.GetByDocument(cmd.document) ??
                throw new CompaignException(HttpStatusCode.NotFound, "Usuário não encontrado.");

            user.UpdateLastAccess();

            _userWriteOnlyRepository.Update(user);
        }
    }
}
