using Domain.DTOs;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsUsernameAvailable(string username);
    
    Graphql.Types.User Read(int id);
    
    List<Graphql.Types.User> ReadAll();
}