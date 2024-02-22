using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Mapster;
using Model.Entities;

namespace Domain.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserFollowsRepository _userFollowsRepository;
    private readonly IJwtService _jwtService;

    public UserService(IUserRepository userRepository, IUserFollowsRepository userFollowsRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _userFollowsRepository = userFollowsRepository;
        _jwtService = jwtService;
    }

    public async Task<UsersResult> Users(UsersInput input)
    {
        return await _userRepository.ReadUsers(input);
    }

    public async Task<UserResult> User(UserInput input)
    {
        return await _userRepository.ReadUser(input);
    }

    public async Task<RegistrationResult> Registration(RegistrationInput input)
    {
        if (!await _userRepository.IsUsernameAvailable(input.Username))
            throw new UsernameAlreadyExistsException(input.Username);

        if (input.Password.Length < 8)
            throw new PasswordTooShortException();
        
        var user = new User {
            Username = input.Username,
            PasswordHash = await HashPassword(input.Password),
            FirstName = input.FirstName,
            LastName = input.LastName
        };

        await _userRepository.CreateAsync(user);
        var tokenString = _jwtService.GenerateToken(user);
        
        var graphqlUser = await _userRepository.ReadGraphqlUser(user.Id);
        if (graphqlUser is null)
            throw new Exception("oh no no no!");
        
        return new RegistrationResult(tokenString, graphqlUser);
    }

    public async Task<LoginResult> Login(LoginInput input)
    {
        var user = await _userRepository.Read(input.Username);
        
        if (user == null)
            throw new UsernameNotFoundException(input.Username);

        if (!await IsAuthenticated(input.Password, user))
            throw new InvalidPasswordException();

        var tokenString = _jwtService.GenerateToken(user);
            
        var graphqlUser = await _userRepository.ReadGraphqlUser(user.Id);
        if (graphqlUser is null)
            throw new Exception("oh no no no!");
        
        return new LoginResult(tokenString, graphqlUser);
    }

    public async Task<AddFollowResult> AddFollow(AddFollowInput input)
    {
        var follower = await _userRepository.Read(input.UserId);
        var following = await _userRepository.Read(input.FollowingId);
        
        if (follower == null) throw new UserNotFoundException(input.UserId);
        if (following == null) throw new FollowingNotFoundException(input.FollowingId);

        if (await _userFollowsRepository.IsFollowing(input.FollowingId, input.UserId))
            throw new AlreadyFollowingException(follower.Username, following.Username); 
        
        var userFollows = new UserFollowsUser()
        {
            FollowingId = following.Id,
            FollowerId = follower.Id
        };
        
        await _userFollowsRepository.CreateAsync(userFollows);

        return new AddFollowResult(follower.Following.Count + 1, follower.Follower.Count);
    }

    public async Task<RemoveFollowResult> RemoveFollow(RemoveFollowInput input)
    {
        var follower = await _userRepository.Read(input.UserId);
        var following = await _userRepository.Read(input.FollowingId);
        
        if (follower == null) throw new UserNotFoundException(input.UserId);
        if (following == null) throw new FollowingNotFoundException(input.FollowingId);

        if (!await _userFollowsRepository.IsFollowing(input.FollowingId, input.UserId))
            throw new NeverFollowedException(follower.Username, following.Username);
        
        await _userFollowsRepository.DeleteAsync(f => f.FollowingId == following.Id && f.FollowerId == follower.Id);

        return new RemoveFollowResult(follower.Following.Count - 1, follower.Follower.Count);
    }
    
    public async Task<string> HashPassword(string password) {
        ArgumentNullException.ThrowIfNull(password);
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    public async Task<bool> IsAuthenticated(string password, User user) {
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(user);
        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }
}