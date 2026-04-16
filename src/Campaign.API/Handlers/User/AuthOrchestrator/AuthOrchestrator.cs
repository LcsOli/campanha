using Campaign.API.Commands.User.Auth;
using Campaign.API.Handlers.User.Auth;
using Campaign.API.Commands.User.Update;
using Campaign.Shared.DTOs.Response.User;
using Campaign.API.Handlers.User.UpdateLastAccess;
using Campaign.API.Handlers.User.InsertScoreByAccess;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;

namespace Campaign.API.Handlers.User.AuthOrchestrator
{
    public class AuthOrchestrator : IAuthOrchestrator
    {
        private readonly IUnityOfWork _unityOfWork;


        private readonly IAuthHandler _authHandler;
        private readonly IInsertScoreByAccessHandler _insertScoreByAccessHandler;
        private readonly IUserUpdateLastAccessHandler _userUpdateLastAccessHandler;

        public AuthOrchestrator(IUnityOfWork unityOfWork,
                                       IAuthHandler authHandler,
                                       IInsertScoreByAccessHandler insertScoreByAccessHandler,
                                       IUserUpdateLastAccessHandler userUpdateLastAccessHandler)
        {
            _unityOfWork = unityOfWork;
            _authHandler = authHandler;
            _insertScoreByAccessHandler = insertScoreByAccessHandler;
            _userUpdateLastAccessHandler = userUpdateLastAccessHandler;
        }

        public async Task<TokenJwtResponse> Execute(AuthOrchestratorCommand cmd)
        {
            var token = await _authHandler.Handle(new AuthCommand(cmd.Document, cmd.Password));
            await _unityOfWork.SecureCommitAsync(async () =>
            {
                await _userUpdateLastAccessHandler.Handle(new UserUpdateLastAccessCommand(cmd.Document));
                await _insertScoreByAccessHandler.Handle(new InsertScoreByAccessCommand(cmd.Document));

                await _unityOfWork.SaveAsync();
            });

            return token;
        }
    }
}