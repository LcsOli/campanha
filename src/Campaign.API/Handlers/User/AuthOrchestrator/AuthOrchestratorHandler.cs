using Campaign.API.Commands.User.Auth;
using Campaign.API.Handlers.User.Auth;
using Campaign.API.Commands.User.Update;
using Campaign.API.Handlers.User.UpdateLastAccess;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.Shared.DTOs.Response.User;

namespace Campaign.API.Handlers.User.AuthOrchestrator
{
    public class AuthOrchestratorHandler : IAuthOrchestratorHandler
    {
        private readonly IUnityOfWork _unityOfWork;

        private readonly IAuthHandler _authHandler;
        private readonly IUserUpdateLastAccessHandler _userUpdateLastAccessHandler;

        public AuthOrchestratorHandler(IUnityOfWork unityOfWork,
                                       IAuthHandler authHandler,
                                       IUserUpdateLastAccessHandler userUpdateLastAccessHandler)
        {
            _unityOfWork = unityOfWork;
            _authHandler = authHandler;
            _userUpdateLastAccessHandler = userUpdateLastAccessHandler;
        }

        public async Task<TokenJwtResponse> Handle(AuthOrchestratorCommand cmd)
        {
            var token = await _authHandler.Handle(new AuthCommand(cmd.Document, cmd.Password));

            await _userUpdateLastAccessHandler.Handle(new UserUpdateLastAccessCommand(cmd.Document));

            await _unityOfWork.SaveAsync();

            return token;
        }
    }
}
