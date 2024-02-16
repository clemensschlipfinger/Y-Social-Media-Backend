using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class YeetRepository : ARepository<Yeet>, IYeetRepository
{
    private readonly IUserFollowsRepository _userFollowsRepository;

    public YeetRepository(YDbContext context, IUserFollowsRepository userFollowsRepository) : base(context)
    {
        _userFollowsRepository = userFollowsRepository;
    }

    public IQueryable<Yeet> ReadFullYeet()
        => Table.Take(50).Include(y => y.User);

    public IQueryable<Yeet> ReadUserYeets(int userId, int count)
        => Table.Where(y => y.UserId == userId).Include(y => y.User).Take(count);

    public IQueryable<Yeet> ReadForYouPage(int userId, int skip, int count)
    {
        var usersIds = _userFollowsRepository.GetFollowing(userId).Select(u => u.Id);
        var yeets = Table
            .Where(y => usersIds.Contains(y.UserId))
            .OrderByDescending(y => y.CreatedAt)
            .Skip(skip)
            .Take(count).Include(u => u.User);
        return yeets;
    }
}