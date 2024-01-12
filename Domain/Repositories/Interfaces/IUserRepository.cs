using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<bool> IsUsernameAvailable(string username);
    
    public IQueryable<User> Read(int id);
    
    public IQueryable<List<User>> ReadAll();
}