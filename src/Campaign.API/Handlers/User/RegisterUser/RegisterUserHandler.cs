using Campaign.API.Services.Password;
using Campaign.API.Commands.User.Create;
using Campaign.API.Repositories.User.WriteOnly;
using Campaign.API.Handlers.User.RegisterUser.Mapper;
using Campaign.API.Handlers.User.RegisterUser.Validator;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Entity = Campaign.Shared.DataBaseContext.Entities.Users;

namespace Campaign.API.Handlers.User.RegisterUser
{
    public class RegisterUserHandler : IRegisterUserHandler
    {
        private readonly IUnityOfWork _unityOfWork;

        private readonly IPasswordService _passwordService;

        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        public RegisterUserHandler(IUnityOfWork unityOfWork,
                                   IPasswordService passwordService,
                                   IUserWriteOnlyRepository userWriteOnlyRepository)
        {
            _unityOfWork = unityOfWork;
            _passwordService = passwordService;
            _userWriteOnlyRepository = userWriteOnlyRepository;
        }

        public async Task<Entity.User> Handle(InsertUserCommand cmd)
        {
            new RegisterUserValidator()
                .Validate(cmd);

            var user = RegisterUserMapper.ToEntity(cmd);
            user.SetPassword(_passwordService.GeneratePassword(user, cmd.Password));

            await _userWriteOnlyRepository.AddAsync(user);

            return user;
        }
    }
}
