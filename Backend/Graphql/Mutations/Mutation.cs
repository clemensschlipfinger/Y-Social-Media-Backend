using Domain.Repositories.Implementations;
using HotChocolate.Authorization;
using Model.Entities;

namespace Backend.Graphql.Mutations;

public class Mutation
{
    [Authorize]
    public async Task<User> AddFollow(int master_id, int slave_id, [Service] IUserFollowsRepository ufuRepo, [Service] IUserRepository userRepo)
    {
        var master = await userRepo.ReadAsync(slave_id);
        
        if (ufuRepo.IsFollowing(master_id, slave_id))
        {
            return master;
        } 
        
        var ufu = new UserFollowsUser()
        {
            MasterId = master_id,
            SlaveId = slave_id
        };
        
        await ufuRepo.CreateAsync(ufu);
        
        return master;
    }
}