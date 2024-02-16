using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class YeetRepository(YDbContext context, IUserFollowsRepository userFollowsRepository) : ARepository<Yeet>(context), IYeetRepository
{

    public async Task<Yeet?> ReadYeet(int id)
        => await Table.FirstOrDefaultAsync(y => y.Id == id);

    public async Task<List<Yeet>> ReadYeets(int userId, int skip, int count)
        => await Table.Where(y => y.UserId == userId).Include(y => y.User).Skip(skip).Take(count).ToListAsync();

    public async Task<List<Yeet>> ReadFeed(int userId, int skip, int count)
    {
        var usersIds = (await userFollowsRepository.GetFollowing(userId)).Select(u => u.Id).ToList();
        return await Table
            .Where(y => usersIds.Contains(y.UserId))
            .OrderByDescending(y => y.CreatedAt)
            .Skip(skip)
            .Take(count)
            .Include(u => u.User)
            .ToListAsync();
    }
}