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
    private readonly IRegexService _regexService;

    public UserService(IUserRepository userRepository, IUserFollowsRepository userFollowsRepository, IJwtService jwtService,
        IRegexService regexService)
    {
        _userRepository = userRepository;
        _userFollowsRepository = userFollowsRepository;
        _jwtService = jwtService;
        _regexService = regexService;
    }

    public async Task<UsersResult> Users(UsersInput input)
    {
        var users = await _userRepository.ReadAll();
        users = users.Where(u =>
                _regexService.Matches(input.Filter, u.Username) ||
                _regexService.Matches(input.Filter, u.FirstName) ||
                _regexService.Matches(input.Filter, u.LastName))
            .ToList();
        users.Sort(delegate(Graphql.Types.User x, Graphql.Types.User y)
        {
            switch (input.Sorting)
            {
                case SortUsers.USERNAME:
                    if (x.Username == null && y.Username == null) return 0;
                    if (x.Username == null) return -1;
                    if (y.Username == null) return 1;
                    return x.Username.CompareTo(y.Username);

                case SortUsers.FIRST_NAME:
                    if (x.FirstName == null && y.FirstName == null) return 0;
                    if (x.FirstName == null) return -1;
                    if (y.FirstName == null) return 1;
                    return x.FirstName.CompareTo(y.FirstName);

                case SortUsers.LAST_NAME:
                    if (x.LastName == null && y.LastName == null) return 0;
                    if (x.LastName == null) return -1;
                    if (y.LastName == null) return 1;
                    return x.LastName.CompareTo(y.LastName);

                case SortUsers.FOLLOWER:
                    if (x.FollowerCount == 0 && y.FollowerCount == 0) return 0;
                    if (x.FollowerCount == 0) return -1;
                    if (y.FollowerCount == 0) return 1;
                    return x.FollowerCount.CompareTo(y.FollowerCount);

                case SortUsers.ID:
                default:
                    if (x.Id == 0 && y.Id == 0) return 0;
                    if (x.Id == 0) return -1;
                    if (y.Id == 0) return 1;
                    return x.Id.CompareTo(y.Id);
            }
        });
        if (input.Direction == SortDirection.DSC) users.Reverse();
        var count = users.Count;
        users = users.Skip(input.Offset).Take(input.Limit).ToList();
        return new UsersResult(users, count);
    }

    public async Task<UserResult> User(UserInput input)
    {
        var user = await _userRepository.Read(input.UserId);
        if (user == null) throw new UserNotFoundException(input.UserId);
        return new UserResult(user);
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
        return new RegistrationResult(tokenString, user.Adapt<Graphql.Types.User>());
    }

    public async Task<LoginResult> Login(LoginInput input)
    {
        var user = await _userRepository.Read(input.Username);
        
        if (user == null)
            throw new UsernameNotFoundException(input.Username);

        if (!await IsAuthenticated(input.Password, user.Adapt<User>()))
            throw new InvalidPasswordException();

        var tokenString = _jwtService.GenerateToken(user.Adapt<User>());
        return new LoginResult(tokenString, user);
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

        return new AddFollowResult(follower.FollowingCount + 1, follower.FollowerCount);
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

        return new RemoveFollowResult(follower.FollowingCount - 1, follower.FollowerCount);
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