using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations;

public class UserService : IUserService
{
    public UsersResult Users(UsersInput input)
    {
        throw new NotImplementedException();
    }

    public UserResult User(UserInput input)
    {
        throw new NotImplementedException();
    }

    public RegistrationResult Registration(RegistrationInput input)
    {
        throw new NotImplementedException();
    }

    public LoginResult Login(LoginInput input)
    {
        throw new NotImplementedException();
    }

    public AddFollowResult AddFollow(AddFollowInput input)
    {
        throw new NotImplementedException();
    }

    public RemoveFollowResult RemoveFollow(RemoveFollowInput input)
    {
        throw new NotImplementedException();
    }
}