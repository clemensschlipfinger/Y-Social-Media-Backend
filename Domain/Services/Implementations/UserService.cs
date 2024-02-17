using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Model.Entities;

namespace Domain.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserFollowsRepository _userFollowsRepository;

    public UserService(IUserRepository userRepository, IUserFollowsRepository userFollowsRepository)
    {
        _userRepository = userRepository;
        _userFollowsRepository = userFollowsRepository;
    }
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
        // var follower = await userRepository.ReadAsync(input.UserId);
        // var following = await userRepository.ReadAsync(input.FollowingId);
        //
        // if (follower == null) throw new UserNotFoundException(input.UserId);
        // if (following == null) throw new FollowingNotFoundException(input.FollowingId);
        //
        // if (userFollowsRepository.IsFollowing(input.FollowingId, input.UserId))
        //     return new AddFollowResult(follower.)
        //
        // var ufu = new UserFollowsUser()
        // {
        //     FollowingId = followingId,
        //     FollowerId = followerId
        // };
        //
        // await ufuRepo.CreateAsync(ufu);
        //
        // return follower!;
        throw new NotImplementedException();
    }

    public RemoveFollowResult RemoveFollow(RemoveFollowInput input)
    {
        throw new NotImplementedException();
    }
}