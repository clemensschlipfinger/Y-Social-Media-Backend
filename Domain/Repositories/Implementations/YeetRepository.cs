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
    private readonly UserFollowsRepository _userFollowsRepository;

    public YeetRepository(YDbContext context, UserFollowsRepository userFollowsRepository) : base(context)
    {
        _userFollowsRepository = userFollowsRepository;
    }

    public async Task<Graphql.Types.Yeet?> ReadYeet(int id)
    {
        var yeet = (await Table.Include(y => y.User).Include(y => y.Yomments).Include(y => y.Tags)
            .FirstOrDefaultAsync(y => y.Id == id));
        if (yeet == null) return null;

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

    public async Task<List<Graphql.Types.Yeet>> ReadYeets(int userId, int skip, int count)
    {
        var yeets = await Table.Include(y => y.User).Include(y => y.Yomments).Include(y => y.Tags)
            .Where(y => y.UserId == userId).Include(y => y.User).Skip(skip).Take(count).ToListAsync();
        var adaptedYeets = new List<Graphql.Types.Yeet>();

        foreach (var yeet in yeets)
        {
            adaptedYeets.Add(new Graphql.Types.Yeet()
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
            });
        }

        return adaptedYeets;
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

        var adaptedYeets = new List<Graphql.Types.Yeet>();

        foreach (var yeet in yeets)
        {
            adaptedYeets.Add(new Graphql.Types.Yeet()
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
            });
        }

        return adaptedYeets;
    }
}