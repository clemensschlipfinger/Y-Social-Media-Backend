using Backend.Identity;
using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using HotChocolate.Authorization;
using HotChocolate.Resolvers;
using Model.Entities;
using IUserService = Domain.Services.Interfaces.IUserService;

namespace Backend.Graphql.Mutations;

public class Mutation
{
    private readonly IUserService _userService;
    private readonly IYeetService _yeetService;
    private readonly IYommentService _yommentService;
    private readonly ITagService _tagService;

    public Mutation(IUserService userService, IYeetService yeetService, IYommentService yommentService, ITagService tagService)
    {
        _userService = userService;
        _yeetService = yeetService;
        _yommentService = yommentService;
        _tagService = tagService;
    }

    [Error(typeof(UsernameAlreadyExistsException))]
    [Error(typeof(PasswordTooShortException))]
    public async Task<RegistrationResult> Registration(RegistrationInput input) => await _userService.Registration(input);

    [Error(typeof(UsernameNotFoundException))]
    [Error(typeof(InvalidPasswordException))]
    public async Task<LoginResult> Login(LoginInput input) => await _userService.Login(input);

    [Authorize]
    [Error(typeof(UserNotFoundException))]
    [Error(typeof(FollowingNotFoundException))]
    [Error(typeof(AlreadyFollowingException))]
    public async Task<AddFollowResult> AddFollow(AddFollowInput input) => await _userService.AddFollow(input);

    [Authorize]
    [Error(typeof(UserNotFoundException))]
    [Error(typeof(FollowingNotFoundException))]
    public async Task<RemoveFollowResult> RemoveFollow(RemoveFollowInput input) => await _userService.RemoveFollow(input);

    [Authorize]
    [Error(typeof(UserNotFoundException))]
    [Error(typeof(TagNotFoundException))]
    public async Task<CreateYeetResult> CreateYeet(CreateYeetInput input) => await _yeetService.CreateYeet(input);

    [Authorize]
    [Error(typeof(YeetNotFoundException))]
    public async Task<DeleteYeetResult> DeleteYeet(DeleteYeetInput input) => await _yeetService.DeleteYeet(input);

    [Authorize]
    [Error(typeof(YeetNotFoundException))]
    [Error(typeof(UserNotFoundException))]
    public async Task<CreateYommentResult> CreateYomment(CreateYommentInput input) =>
        await _yommentService.CreateYomment(input);

    [Authorize]
    [Error(typeof(YommentNotFoundException))]
    public async Task<DeleteYommentResult> DeleteYomment(DeleteYommentInput input) =>
        await _yommentService.DeleteYomment(input);

    [Authorize]
    [Error(typeof(TagAlreadyExistsException))]
    public async Task<CreateTagResult> CreateTag(CreateTagInput input) => await _tagService.CreateTag(input);
}


/*

public class Mutation
{
    [Authorize]
    public async Task<User> AddFollow(int followingId, int followerId, [Service] IUserFollowsRepository ufuRepo, [Service] IUserRepository userRepo)
    {
        
    }
    
    [Error(typeof(UserNotFoundException))]
    [Error(typeof(InvalidPasswordException))]
    public async Task<TokenResponse> Login(string username, string password, [Service] IJwtService jwtService, [Service] IUserService userService)
    { 
        var storedUser = await userService.GetUser(username);
        if (storedUser == null)
            throw new UserNotFoundException(username);

        if (!userService.IsAuthenticated(password, storedUser))
            throw new InvalidPasswordException();

        var tokenString = jwtService.GenerateToken(storedUser);
        return new TokenResponse(tokenString, storedUser);
    }

    [Error(typeof(UsernameAlreadyExistsException))]
    [Error(typeof(PasswordTooShortException))]
    public async Task<User> Registration(string username, string firstname, string lastname, string password,
        [Service] IUserRepository userRepo, [Service] IUserService userService) {
        if (!userRepo.IsUsernameAvailable(username).Result)
            throw new UsernameAlreadyExistsException(username);

        if (password.Length < 8) 
            throw new PasswordTooShortException();

        var user = new User {
            Username = username,
            PasswordHash = userService.HashPassword(password),
            FirstName = firstname,
            LastName = lastname
        };

        await userRepo.CreateAsync(user);

        return user;
    }

    public async Task<Yeet> CreateYeet(int userId, string text,[Service] IYeetRepository yeetRepo)
    {
        var yeet = new Yeet
        {
            Body = text,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UserId = userId
        };

        return await yeetRepo.CreateAsync(yeet);
    }

    public async Task<int> DeleteYeet(int yeetId, [Service] IYeetRepository yeetRepo)
    {
        await yeetRepo.DeleteAsync(y => y.Id == yeetId);
        return yeetId;
    }
}
*/