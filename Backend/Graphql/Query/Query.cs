using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.Configuration;
using Model.Entities;

namespace TestGraphql;

public class Query
{
    public IQueryable<Yeet> GetYeets([Service] IYeetRepository repo)
        => repo.ReadFullYeet();
    
    public IQueryable<User> GetFollowers(int userId, [Service] IUserFollowsRepository repo)
        => repo.GetFollowers(userId);
    
    public IQueryable<User> GetFollowing(int userId, [Service] IUserFollowsRepository repo)
        => repo.GetFollowing(userId);
    
    [Authorize]
    public IQueryable<User> GetUser(int userId,[Service] IUserRepository repo)
        => repo.Read(userId);
    
    
}