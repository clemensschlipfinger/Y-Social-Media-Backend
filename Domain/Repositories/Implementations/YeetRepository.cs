using System.Linq.Expressions;
using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Mapper;
using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class YeetRepository
    : ARepository<Yeet>, IYeetRepository
{
    private readonly FromMapper<Yeet, Graphql.Types.Yeet> _mapper;
    private readonly IUserRepository _userRepository;

    public YeetRepository(IDbContextFactory<YDbContext> dbContextFactory, FromMapper<Yeet, Graphql.Types.Yeet> mapper, IUserRepository userRepository) : base(dbContextFactory)
    {
        this._mapper = mapper;
        _userRepository = userRepository;
    }

    private IQueryable<Yeet> PreparedStatement() => Table .Include(y => y.User)
                    .Include(y => y.Tags).ThenInclude(y => y.Tag)
                    .Include(y => y.Yomments).ThenInclude(y => y.User)
                    .AsQueryable(); 

    public async Task<YeetsResult> ReadYeets(YeetsInput input)
    {
        var yeetsQuery = PreparedStatement(); 

        if (input.Filter is not null && input.Filter.Length > 0)
            yeetsQuery = yeetsQuery.Where(u => u.Title.Contains(input.Filter));

        if (input.Tags is not null && input.Tags.Any())
            yeetsQuery = yeetsQuery.Where(u => u.Tags.Any(t => input.Tags.Contains(t.TagId)));

        yeetsQuery = input.Direction switch
        {
            SortDirection.ASC => input.Sorting switch
                {
                    SortYeets.ID => yeetsQuery.OrderBy(y => y.Id),
                    SortYeets.TITLE => yeetsQuery.OrderBy(y => y.Title).ThenBy(y => y.Id),
                    SortYeets.CREATED_AT => yeetsQuery.OrderBy(y => y.CreatedAt).ThenBy(y => y.Id),
                    SortYeets.LIKES => yeetsQuery.OrderBy(y => y.Likes).ThenBy(y => y.Id),
                    SortYeets.TAG => yeetsQuery.OrderBy(y => y.Tags).ThenBy(y => y.Id),
                    _ => throw new ArgumentOutOfRangeException()
                },
            SortDirection.DSC => input.Sorting switch
            {
                SortYeets.ID => yeetsQuery.OrderByDescending(y => y.Id),
                SortYeets.TITLE => yeetsQuery.OrderByDescending(y => y.Title).ThenByDescending(y => y.Id),
                SortYeets.CREATED_AT => yeetsQuery.OrderByDescending(y => y.CreatedAt).ThenByDescending(y => y.Id),
                SortYeets.LIKES => yeetsQuery.OrderByDescending(y => y.Likes).ThenByDescending(y => y.Id),
                SortYeets.TAG => yeetsQuery.OrderByDescending(y => y.Tags).ThenByDescending(y => y.Id),
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => yeetsQuery
        };

        yeetsQuery = yeetsQuery.Skip(input.Offset).Take(input.Limit);

        var count = await yeetsQuery.CountAsync();
        var yeets = await yeetsQuery.ToListAsync();

        return new YeetsResult(yeets.Select(y => _mapper.mapFrom(y)).ToList(), count);
    }

    public async Task<YeetResult> ReadYeet(YeetInput input)
    {
        var yeet = await ReadGraphqlYeet(input.YeetId); 
        return new YeetResult(yeet);
    }

    public async Task<FeedResult> ReadFeed(FeedInput input)
    {
        var following = (await _userRepository.Read(input.UserId))?.Following.Select(u => u.FollowingId).ToList();
        if(following is null || following.Count < 1)
            return new FeedResult(new List<Graphql.Types.Yeet>(), 0);

        var yeetsQuery = PreparedStatement() 
            .Where(yeet => following.Contains(yeet.UserId))
            .OrderByDescending(y => y.CreatedAt)
            .Skip(input.Offset)
            .Take(input.Limit);

        var counts = await yeetsQuery.CountAsync();
        var yeets = await yeetsQuery.ToListAsync();

        return new FeedResult(
            yeets.Select(y => _mapper.mapFrom(y)).ToList(),
            counts
        );
    }

    public async Task<Graphql.Types.Yeet?> ReadGraphqlYeet(int id)
    {
        var yeet = await PreparedStatement().FirstOrDefaultAsync(y => y.Id == id);
        if (yeet is null) { return null; }

        return _mapper.mapFrom(yeet);
    }

    public Task<bool> Exists(int yeetId) => Table.AnyAsync(y => yeetId == y.Id);
}