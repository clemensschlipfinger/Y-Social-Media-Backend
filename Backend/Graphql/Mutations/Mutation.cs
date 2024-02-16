using Backend.Graphql.Types;
using Backend.Graphql.Types.Exceptions;
using Backend.Identity;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using HotChocolate.Authorization;
using HotChocolate.Resolvers;
using Model.Entities;

namespace Backend.Graphql.Mutations;

public class Mutation
{
    [Authorize]
    public async Task<User> AddFollow(int followingId, int followerId, [Service] IUserFollowsRepository ufuRepo, [Service] IUserRepository userRepo)
    {
        var follower = await userRepo.ReadAsync(followerId);
        
        if (ufuRepo.IsFollowing(followingId, followerId))
            return follower!;
        
        var ufu = new UserFollowsUser()
        {
            FollowingId = followingId,
            FollowerId = followerId
        };
        
        await ufuRepo.CreateAsync(ufu);
        
        return follower!;
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