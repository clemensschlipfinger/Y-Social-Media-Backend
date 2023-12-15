using Backend.Identity;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using HotChocolate.Authorization;
using Model.Entities;

namespace Backend.Graphql.Mutations;

public class Mutation {
    [Authorize]
    public async Task<User> AddFollow(int master_id, int slave_id, [Service] IUserFollowsRepository ufuRepo,
        [Service] IUserRepository userRepo) {
        var master = await userRepo.ReadAsync(slave_id);

        if (ufuRepo.IsFollowing(master_id, slave_id)) {
            return master;
        }

        var ufu = new UserFollowsUser() {
            MasterId = master_id,
            SlaveId = slave_id
        };

        await ufuRepo.CreateAsync(ufu);

        return master;
    }

    [Error(typeof(UsernameAlreadyTakenException))]
    [Error(typeof(PasswordTooShortException))]
    public async Task<User> UserRegistration(string username, string firstname, string lastname, string password,
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