using Domain.DTOs;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsUsernameAvailable(string username);

    Task<User?> Read(int id);
    Task<User?> Read(string username);
    
    Task<Graphql.Types.User?> ReadGraphqlUser(int id);
    Task<Graphql.Types.User?> ReadGraphqlUser(string username);

    Task<UserResult> ReadUser(UserInput input);
    Task<UserByUserNameResult> ReadUser(UserByUserNameInput input);
    Task<UsersResult> ReadUsers(UsersInput input);

    Task<bool> Exists(int userId);
}