using Domain.DTOs;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IUserFollowsRepository : IRepository<UserFollowsUser>
{
    public IQueryable<User> GetFollowers(int user_id);
    public IQueryable<User> GetFollowing(int user_id);
    public CountUserDto GetFollowingCount(int userId);
    public CountUserDto GetFollowersCount(int userId);
    public bool IsFollowing(int master_id, int slave_id);

}