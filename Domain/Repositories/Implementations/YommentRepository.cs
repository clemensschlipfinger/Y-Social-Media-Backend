using Domain.Repositories.Interfaces;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class YommentRepository : ARepository<Yomment>, IYommentRepository
{
    public YommentRepository(YDbContext context) : base(context)
    {
    }
}