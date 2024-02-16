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
    public UserRepository(YDbContext context, IUserFollowsRepository userFollowsRepository) : base(context) {
        _userFollowsRepository = userFollowsRepository;
    }
    
    public async Task<bool> IsUsernameAvailable(string username)
    {
        return await Table.AllAsync(u => u.Username != username);
    }
    
    public IQueryable<User> Read(int id) {
        return Table.Where(e => e.Id == id);
    }

    public IQueryable<DefaultUserDto> ReadAll()
    {
        return Table.Select(u => u.Adapt<DefaultUserDto>());
    }

    public IQueryable<FullUserDto> ReadFullUsers() {
        var users = Table.ToList();
        List<ExpandedUser> expUser = new();
        foreach (var user in users) {
            var followers = _userFollowsRepository.GetFollowers(user.Id).Select(f => f.Adapt<DefaultUserDto>()).ToList();
            var following = _userFollowsRepository.GetFollowing(user.Id).Select(f => f.Adapt<DefaultUserDto>()).ToList();
            expUser.Add(new ExpandedUser {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Id = user.Id,
                Followers = followers,
                Followings = following,
                FollowerCount = followers.Count(),
                FollowingCount = following.Count()
            });
        }
        return expUser.Select(eu => eu.Adapt<FullUserDto>()).AsQueryable();
    }
}