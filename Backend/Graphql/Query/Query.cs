using Domain.DTOs;
using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using HotChocolate.Authorization;
using Mapster;
using Model.Entities;

namespace Backend.Graphql.Query;

public class Query
{
    public IQueryable<Yeet> GetYeets([Service] IYeetRepository repo)
        => repo.ReadFullYeet();

    public IQueryable<User> GetFollowers(int userId, [Service] IUserFollowsRepository repo)
        => repo.GetFollowers(userId);

    public IQueryable<User> GetFollowing(int userId, [Service] IUserFollowsRepository repo)
        => repo.GetFollowing(userId);

    [Authorize]
    public IQueryable<User> GetUser(int userId, [Service] IUserRepository repo)
        => repo.Read(userId);

    public IQueryable<DefaultUserDto> GetUsers([Service] IUserRepository repo)
        => repo.ReadAll();

    public CountUserDto GetFollowingCount(int userId, [Service] IUserFollowsRepository repo)
        => repo.GetFollowingCount(userId);

    public CountUserDto GetFollowerCount(int userId, [Service] IUserFollowsRepository repo)
        => repo.GetFollowersCount(userId);

    public IQueryable<Yeet> GetForYouPage(int userId, int skip, int count, [Service] IYeetRepository repo) =>
        repo.ReadForYouPage(userId, skip, count);
}