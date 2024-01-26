using System.Linq.Expressions;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IYeetRepository  : IRepository<Yeet>
{
    IQueryable<Yeet> ReadFullYeet();

    IQueryable<Yeet> ReadUserYeets(int userId, int count);

    IQueryable<Yeet> ReadForYouPage(int userId, int skip,int count);
}