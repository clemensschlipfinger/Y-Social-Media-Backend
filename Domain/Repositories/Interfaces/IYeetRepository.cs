using System.Linq.Expressions;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IYeetRepository  : IRepository<Yeet>
{
    IQueryable<Yeet> ReadFullYeet();
}