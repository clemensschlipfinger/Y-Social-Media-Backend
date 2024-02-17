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
        var followers = (await _userFollowsRepository.GetFollowers(id)).Select(f => f.Adapt<Graphql.Types.UserInfo>()).ToList();
        var following = (await _userFollowsRepository.GetFollowing(id)).Select(f => f.Adapt<Graphql.Types.UserInfo>()).ToList();
        return new Graphql.Types.User
        {
            Id = user.Id, Username = user.Username, FirstName = user.FirstName, LastName = user.LastName,
            FollowerCount = followers.Count(), FollowingCount = following.Count(), Follower = followers,
            Following = following
        };
    }

    public async Task<Graphql.Types.User?> Read(string username)
    {
        var user = await Table.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return null;
        var followers = (await _userFollowsRepository.GetFollowers(user.Id)).Select(f => f.Adapt<Graphql.Types.UserInfo>()).ToList();
        var following = (await _userFollowsRepository.GetFollowing(user.Id)).Select(f => f.Adapt<Graphql.Types.UserInfo>()).ToList();
        return new Graphql.Types.User
        {
            Id = user.Id, Username = user.Username, FirstName = user.FirstName, LastName = user.LastName,
            FollowerCount = followers.Count(), FollowingCount = following.Count(), Follower = followers,
            Following = following
        };
    }

    public async Task<List<Graphql.Types.User>> ReadAll()
    {
        var userInfos = await Table.ToListAsync();
        
        var users = new List<Graphql.Types.User>();
        foreach (var user in userInfos)
        {
            var followers = (await _userFollowsRepository.GetFollowers(user.Id)).Select(f => f.Adapt<Graphql.Types.UserInfo>()).ToList();
            var following = (await _userFollowsRepository.GetFollowing(user.Id)).Select(f => f.Adapt<Graphql.Types.UserInfo>()).ToList();
            users.Add(new Graphql.Types.User
            {
                Id = user.Id, Username = user.Username, FirstName = user.FirstName, LastName = user.LastName,
                FollowerCount = followers.Count(), FollowingCount = following.Count(), Follower = followers,
                Following = following
            });
        }

        return users;
    }
}