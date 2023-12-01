using Domain.Repositories.Implementations;
using Domain.Repositories.Interfaces;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace TestGraphql;

public class Query
{
    public IQueryable<Yeet> GetYeets([Service] IYeetRepository repo)
        => repo.ReadFullYeet();
    
    public IQueryable<User> GetFollowers(int user_id, [Service] IUserFollowsRepository repo)
        => repo.GetFollowers(user_id);
}