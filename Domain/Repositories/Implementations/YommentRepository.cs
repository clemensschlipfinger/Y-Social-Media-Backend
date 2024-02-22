using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class YommentRepository : ARepository<Yomment>, IYommentRepository
{
    public YommentRepository(IDbContextFactory<YDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}