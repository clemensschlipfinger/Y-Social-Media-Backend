using Domain.Repositories.Implementations;
using Model.Entities;

namespace Backend.Graphql.Mutations;

public class Mutation
{
    public async Task<User> AddFollow(int master_id, int slave_id, [Service] IUserFollowsRepository ufuRepo, [Service] UserRepository userRepo)
    {
        var master = await userRepo.ReadAsync(slave_id);
        
        var ufu = new UserFollowsUser()
        {
            MasterId = master_id,
            SlaveId = slave_id
        };
            
        await ufuRepo.CreateAsync(ufu);
        return master;
    }
}