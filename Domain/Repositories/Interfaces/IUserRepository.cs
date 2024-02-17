using Domain.DTOs;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsUsernameAvailable(string username);
    
    Task<Graphql.Types.User?> Read(int id);
    
    Task<Graphql.Types.User?> Read(string username);
    
    Task<List<Graphql.Types.User>> ReadAll();
}