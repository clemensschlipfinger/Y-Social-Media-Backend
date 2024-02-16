using System.Linq.Expressions;
using Domain.DTOs;
using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class UserFollowsRepository : ARepository<UserFollowsUser>, IUserFollowsRepository
{
    public UserFollowsRepository(YDbContext context) : base(context) { }

    public IQueryable<User> GetFollowers(int user_id)
        => Table.Where((ufu) => ufu.FollowingId == user_id)
            .Include(ufu => ufu.Follower)
            .Select(ufu => ufu.Follower);

    public IQueryable<User> GetFollowing(int user_id)
        => Table.Where((ufu) => ufu.FollowerId== user_id)
            .Include(ufu => ufu.Following)
            .Select(ufu => ufu.Following);

    public bool IsFollowing(int following_id, int follower_id)
        => Table.FirstOrDefault(u => u.FollowingId == following_id && u.FollowerId == follower_id) != null;
    
}
