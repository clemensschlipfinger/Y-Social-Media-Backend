using System.Linq.Expressions;
using Domain.DTOs;
using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class UserFollowsRepository(YDbContext context) : ARepository<UserFollowsUser>(context), IUserFollowsRepository
{

    public async Task<List<User>> GetFollowers(int userId)
        => await Table.Where((ufu) => ufu.FollowingId == userId)
            .Include(ufu => ufu.Follower)
            .Select(ufu => ufu.Follower).ToListAsync();

    public async Task<List<User>> GetFollowing(int userId)
        => await Table.Where((ufu) => ufu.FollowerId== userId)
            .Include(ufu => ufu.Following)
            .Select(ufu => ufu.Following).ToListAsync();

    public async Task<bool> IsFollowing(int followingId, int followerId)
        => await Table.FirstOrDefaultAsync(u => u.FollowingId == followingId && u.FollowerId == followerId) != null;
}
