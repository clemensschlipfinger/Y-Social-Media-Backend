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
        => base.Read((ufu) => ufu.MasterId == user_id)
            .Include(ufu => ufu.Slave)
            .Select(ufu => ufu.Slave);

    public IQueryable<User> GetFollowing(int user_id)
        => base.Read((ufu) => ufu.SlaveId== user_id)
            .Include(ufu => ufu.Master)
            .Select(ufu => ufu.Master);

    public CountUserDto GetFollowingCount(int userId)
    {
        var users = GetFollowing(userId);
        var count = users.Count();
        var usersDto = users.Select(u => u.Adapt<DefaultUserDto>());
        return new CountUserDto(Count: count, Users: usersDto);
    }

    public CountUserDto GetFollowersCount(int userId)
    {
        var users = GetFollowers(userId);
        var count = users.Count();
        var usersDto = users.Select(u => u.Adapt<DefaultUserDto>());
        return new CountUserDto(Count: count, Users: usersDto);
    }

    public bool IsFollowing(int master_id, int slave_id)
        => Table.Find(master_id, slave_id) != null;
    
}
