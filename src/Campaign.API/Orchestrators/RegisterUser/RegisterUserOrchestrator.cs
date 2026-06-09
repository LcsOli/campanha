using Campaign.Shared.Enums.Role;
using Campaign.API.Commands.User.Create;
using Campaign.API.Handlers.User.RegisterUser;
using Campaign.API.Commands.SellerScore.Create;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.API.Handlers.SellerScore.RegisterSellerScore;
using Entity = Campaign.Shared.DataBaseContext.Entities.Users;

namespace Campaign.API.Orchestrators.RegisterUser
{
    public class RegisterUserOrchestrator : IRegisterUserOrchestrator
    {
        private readonly IUnityOfWork _unityOfWork;

        private readonly IRegisterUserHandler _registerUserHandler;
        private readonly IRegisterSellerScoreHandler _registerSellerScoreHandler;

        public RegisterUserOrchestrator(IUnityOfWork unityOfWork,
                                        IRegisterUserHandler registerUserHandler,
                                        IRegisterSellerScoreHandler registerSellerScoreHandler)
        {
            _unityOfWork = unityOfWork;
            _registerUserHandler = registerUserHandler;
            _registerSellerScoreHandler = registerSellerScoreHandler;
        }

        public async Task<int> Execute(RegisterUserCommand cmd)
        {

            var user = default(Entity.User);

            await _unityOfWork.SecureCommitAsync(async () =>
            {
                user = await _registerUserHandler.Handle(new InsertUserCommand(cmd.Role,
                                                                               cmd.TeamId,
                                                                               cmd.Name,
                                                                               cmd.SellerId,
                                                                               cmd.SupplierId,
                                                                               cmd.Document,
                                                                               cmd.Password));
                await _unityOfWork.SaveAsync();

                if (cmd.Role == Roles.User)
                {
                    await _registerSellerScoreHandler.Handler(new RegisterSellerScoreCommand(Name: cmd.Name,
                                                                                             TeamId: cmd.TeamId!.Value,
                                                                                             SellerId: cmd.SellerId!.Value,
                                                                                             SellerManagerName: cmd.SellerManagerName!,
                                                                                             SellerManagerId: cmd.SellerManagerId!.Value));

                    await _unityOfWork.SaveAsync();
                }

            });

            return user!.Id;
        }
    }
}
