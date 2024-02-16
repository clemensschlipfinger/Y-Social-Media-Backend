using System.Linq.Expressions;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IYeetRepository  : IRepository<Yeet>
{
    Task<Yeet?> ReadYeet(int id);

    Task<List<Yeet>> ReadYeets(int userId, int skip, int count);

    Task<List<Yeet>> ReadFeed(int userId, int skip, int count);
}