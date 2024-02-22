using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class YeetRepository
    : ARepository<Yeet>, IYeetRepository
{
    private readonly IUserFollowsRepository _userFollowsRepository;
    public YeetRepository(IDbContextFactory<YDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }


    public async Task<Graphql.Types.Yeet?> ReadYeet(int id)
    {
        var yeet = (await Table.Include(y => y.User)
            .Include(y => y.Yomments)
            .Include(y => y.Tags)
            .FirstOrDefaultAsync(y => y.Id == id));
        if (yeet == null) return null;
        return adaptFromYeet(yeet);
    }

    public async Task<List<Graphql.Types.Yeet>> ReadYeets(int userId, int skip, int count)
    {
        var yeets = await Table.Include(y => y.User)
            .Include(y => y.Yomments)
            .Include(y => y.Tags)
            .Where(y => y.UserId == userId)
            .Include(y => y.User)
            .Skip(skip)
            .Take(count)
            .ToListAsync();
        
        return yeets.Select(adaptFromYeet).ToList();
    }

    public async Task<List<Graphql.Types.Yeet>> ReadFeed(int userId, int skip, int count)
    {
        var usersIds = (await _userFollowsRepository.GetFollowing(userId)).Select(u => u.Id).ToList();
        var yeets = await Table
            .Where(y => usersIds.Contains(y.UserId))
            .OrderByDescending(y => y.CreatedAt)
            .Skip(skip)
            .Take(count)
            .Include(u => u.User)
            .ToListAsync();

        return yeets.Select(adaptFromYeet).ToList();
    }

    private Graphql.Types.Yeet adaptFromYeet(Yeet yeet)
    {
        return new Graphql.Types.Yeet()
        {
            Id = yeet.Id,
            Title = yeet.Title,
            Body = yeet.Body,
            CreatedAt = yeet.CreatedAt,
            Likes = yeet.Likes,
            UserId = yeet.UserId,
            User = yeet.User.Adapt<Graphql.Types.User>(),
            Yomments = yeet.Yomments.Select(y => y.Adapt<Graphql.Types.Yomment>()).ToList(),
            Tags = yeet.Tags.Select(y => y.Tag.Adapt<Graphql.Types.Tag>()).ToList()
        };
    }

}