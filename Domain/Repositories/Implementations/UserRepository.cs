using Domain.DTOs;
using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class UserRepository : ARepository<User>, IUserRepository
{
    private readonly IUserFollowsRepository _userFollowsRepository;

    public UserRepository(YDbContext context, IUserFollowsRepository userFollowsRepository) : base(context)
    {
        _userFollowsRepository = userFollowsRepository;
    }

    public async Task<bool> IsUsernameAvailable(string username)
    {
        return await Table.AllAsync(u => u.Username != username);
    }

    public async Task<Graphql.Types.User?> Read(int id)
    {
        var user = await Table.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;

        return await Read(user);
    }

    public async Task<Graphql.Types.User?> Read(string username)
    {
        var user = await Table.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return null;
        
        return await Read(user);
    }

    public async Task<List<Graphql.Types.User>> ReadAll()
    {
        var users = Table.Select(u => Read(u)).ToList();
        return (await Task.WhenAll(users)).ToList();
    }

    public async Task<User> ReadForAuth(string username)
    {
        var user = await Table.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return null;
        return user;
    }

    public async Task<User> ReadForAuth(int id)
    {
        var user = await Table.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        return user;
    }

    public async Task<Graphql.Types.User?> Read(User user)
    {
        var followers = (await _userFollowsRepository.GetFollowers(user.Id)).Select(f => f.Adapt<Graphql.Types.UserInfo>()).ToList();
        var following = (await _userFollowsRepository.GetFollowing(user.Id)).Select(f => f.Adapt<Graphql.Types.UserInfo>()).ToList();
        return new Graphql.Types.User
        {
            Id = user.Id, Username = user.Username, FirstName = user.FirstName, LastName = user.LastName,
            FollowerCount = followers.Count(), FollowingCount = following.Count(), Follower = followers,
            Following = following
        };
    }
}