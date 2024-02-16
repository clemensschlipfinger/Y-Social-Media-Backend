using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Model.Entities;

namespace Domain.Services.Implementations;

public class UserService(IUserRepository userRepository, IUserFollowsRepository userFollowsRepository) : IUserService
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

    public async Task<AddFollowResult> AddFollow(AddFollowInput input)
    {
        var follower = await userRepository.ReadAsync(input.UserId);

        if (follower == null) throw new UserNotFoundException();
        
        
        if (userFollowsRepository.IsFollowing(input.FollowingId, input.UserId))
            return new AddFollowResult()
        
        var ufu = new UserFollowsUser()
        {
            FollowingId = followingId,
            FollowerId = followerId
        };
        
        await ufuRepo.CreateAsync(ufu);
        
        return follower!;
    }

    public RemoveFollowResult RemoveFollow(RemoveFollowInput input)
    {
        throw new NotImplementedException();
    }
}