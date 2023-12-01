using Domain.Repositories.Interfaces;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public interface IUserFollowsRepository : IRepository<UserFollowsUser>
{
    public IQueryable<User> GetFollowers(int user_id);
}