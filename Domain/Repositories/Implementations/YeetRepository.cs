using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class YeetRepository : ARepository<Yeet>, IYeetRepository
{
    public YeetRepository(YDbContext context) : base(context) { }

    public IQueryable<Yeet> ReadFullYeet()
        => base.Read().Include(y => y.User).Take(50);
}