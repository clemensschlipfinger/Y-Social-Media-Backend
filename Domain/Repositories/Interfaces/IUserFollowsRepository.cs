using Domain.DTOs;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IUserFollowsRepository : IRepository<UserFollowsUser>
{
    Task<List<User>> GetFollowers(int userId);
    Task<List<User>> GetFollowing(int userId);
    Task<bool> IsFollowing(int followingId, int followerId);

}