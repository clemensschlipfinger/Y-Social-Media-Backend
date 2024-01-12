using Backend.Identity;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using HotChocolate.Authorization;
using Model.Entities;

namespace Backend.Graphql.Mutations;

public class Mutation
{
    [Authorize]
    public async Task<User> AddFollow(int masterId, int slaveId, [Service] IUserFollowsRepository ufuRepo, [Service] IUserRepository userRepo)
    {
        var master = await userRepo.ReadAsync(slaveId);
        
        if (ufuRepo.IsFollowing(masterId, slaveId))
            return master!;
        
        var ufu = new UserFollowsUser()
        {
            MasterId = masterId,
            SlaveId = slaveId
        };
        
        await ufuRepo.CreateAsync(ufu);
        
        return master!;
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

    [Error(typeof(UsernameAlreadyTakenException))]
    [Error(typeof(PasswordTooShortException))]
    public async Task<User> Registration(string username, string firstname, string lastname, string password,
        [Service] IUserRepository userRepo, [Service] IUserService userService) {
        if (!userRepo.IsUsernameAvailable(username).Result)
            throw new UsernameAlreadyTakenException(username);

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
}