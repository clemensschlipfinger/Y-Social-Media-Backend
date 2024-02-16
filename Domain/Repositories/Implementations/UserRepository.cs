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

    public Graphql.Types.User Read(int id)
    {
        return Table.Where(e => e.Id == id);
    }

    public IQueryable<UserInfoDto> ReadAll()
    {
        return Table.Select(u => u.Adapt<UserInfoDto>());
    }

    public IQueryable<UserDto> ReadFullUsers()
    {
        var users = Table.ToList();
        List<UserDto> expUser =
            (from user in users
                let followers =
                    _userFollowsRepository.GetFollowers(user.Id).Select(f => f.Adapt<UserInfoDto>()).ToList()
                let following = _userFollowsRepository.GetFollowing(user.Id).Select(f => f.Adapt<UserInfoDto>())
                    .ToList()
                select new UserDto(
                    user.Id,
                    user.Username,
                    user.FirstName,
                    user.LastName,
                    followers.Count,
                    following.Count,
                    followers,
                    following)).ToList();

        return expUser.Select(eu => eu.Adapt<UserDto>()).AsQueryable();
    }
}