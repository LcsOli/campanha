using Campaign.API.Commands.User.Create;
using Campaign.API.Repositories.User.WriteOnly;
using Campaign.API.Handlers.RegisterUser.Validator;

namespace Campaign.API.Handlers.RegisterUser
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

           
        }
    }
}
