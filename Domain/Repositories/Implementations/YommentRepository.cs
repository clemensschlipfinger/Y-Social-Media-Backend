using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Mapper;
using Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;

namespace Domain.Repositories.Implementations;

public class YommentRepository : ARepository<Yomment>, IYommentRepository
{
    private readonly FromMapper<Yomment, Graphql.Types.Yomment> _yommentMapper;
    public YommentRepository(IDbContextFactory<YDbContext> dbContextFactory, FromMapper<Yomment, Graphql.Types.Yomment> yommentMapper) : base(dbContextFactory)
    {
        _yommentMapper = yommentMapper;
    }
    
    private IQueryable<Yomment> PreparedStatement() => Table .Include(y => y.User) .AsQueryable(); 

    public async Task<YommentsResult> ReadYomments(YommentsInput input) 
    {
        var yommentsQuery = PreparedStatement();

        yommentsQuery = yommentsQuery.Where(y => y.YeetId == input.YeetId);

        yommentsQuery = input.Direction switch
        {
            SortDirection.ASC => input.Sorting switch
            {
                SortYomments.ID => yommentsQuery.OrderBy(y => y.Id),
                SortYomments.CREATED_AT => yommentsQuery.OrderBy(y => y.CreatedAt),
                SortYomments.LIKES => yommentsQuery.OrderBy(y => y.Likes),
                _ => throw new ArgumentOutOfRangeException()
            },
            SortDirection.DSC => input.Sorting switch
            {
                SortYomments.ID => yommentsQuery.OrderByDescending(y => y.Id),
                SortYomments.CREATED_AT => yommentsQuery.OrderByDescending(y => y.CreatedAt),
                SortYomments.LIKES => yommentsQuery.OrderByDescending(y => y.Likes),
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => throw new ArgumentOutOfRangeException()
        };
        
        yommentsQuery = yommentsQuery.Skip(input.Offset).Take(input.Limit);

        var count = await yommentsQuery.CountAsync();
        var yomments = await yommentsQuery.ToListAsync();

        return new YommentsResult(yommentsQuery.Select(y => _yommentMapper.mapFrom(y)).ToList(), count);
    }

    public async Task<YommentResult> ReadYomment(YommentInput input)
    {
        var yeet = await ReadGraphqlYomment(input.YommentId);
        return new YommentResult(yeet);
    }

    public async Task<Graphql.Types.Yomment?> ReadGraphqlYomment(int id)
    {
        var yeet = await PreparedStatement().FirstOrDefaultAsync(y => y.Id == id);
        if (yeet is null) { return null; }

        return _yommentMapper.mapFrom(yeet);
    }

    public Task<bool> Exists(int yommentId) => Table.AnyAsync(y => y.Id == yommentId);
}