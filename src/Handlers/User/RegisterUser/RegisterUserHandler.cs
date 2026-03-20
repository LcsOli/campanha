using Campaign.API.Commands.User.Create;
using Campaign.API.Repositories.User.WriteOnly;
using Campaign.API.Handlers.User.RegisterUser.Mapper;
using Campaign.API.Handlers.User.RegisterUser.Validator;

namespace Campaign.API.Handlers.User.RegisterUser
{
    public class RegisterUserHandler : IRegisterUserHandler
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        public RegisterUserHandler(IUserWriteOnlyRepository userWriteOnlyRepository)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
        }

        public async Task Handle(RegisterUserCommand cmd)
        {
            new RegisterUserValidator()
                .Validate(cmd);

            var user = RegisterUserMapper.ToEntity(cmd);

            await _userWriteOnlyRepository.Add(user);
        }
    }
}
