using System.Linq.Expressions;
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
}
