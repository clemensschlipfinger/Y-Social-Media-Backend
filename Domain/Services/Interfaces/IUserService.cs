using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;

namespace Domain.Services.Interfaces;

public interface IUserService
{
    Task<UsersResult> Users(UsersInput input);
    Task<UserResult> User(UserInput input);
    Task<RegistrationResult> Registration(RegistrationInput input);
    Task<LoginResult> Login(LoginInput input);
    Task<AddFollowResult> AddFollow(AddFollowInput input);
    Task<RemoveFollowResult> RemoveFollow(RemoveFollowInput input);
}