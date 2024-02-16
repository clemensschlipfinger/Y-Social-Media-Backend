using System.Linq.Expressions;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IYeetRepository  : IRepository<Yeet>
{
    Task<Graphql.Types.Yeet?> ReadYeet(int id);

    Task<List<Graphql.Types.Yeet>> ReadYeets(int userId, int skip, int count);

    Task<List<Graphql.Types.Yeet>> ReadFeed(int userId, int skip, int count);
}