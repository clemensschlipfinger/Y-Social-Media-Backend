using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;

namespace Domain.Services.Interfaces;

public interface IUserService
{
    UsersResult Users(UsersInput input);
    UserResult User(UserInput input);
    RegistrationResult Registration(RegistrationInput input);
    LoginResult Login(LoginInput input);
    AddFollowResult AddFollow(AddFollowInput input);
    RemoveFollowResult RemoveFollow(RemoveFollowInput input);
}