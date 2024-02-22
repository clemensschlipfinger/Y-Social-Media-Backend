using System.Linq.Expressions;
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
    private readonly FromMapper<Yeet, Graphql.Types.Yeet> mapper;

    public YeetRepository(IDbContextFactory<YDbContext> dbContextFactory, FromMapper<Yeet, Graphql.Types.Yeet> mapper) : base(dbContextFactory)
    {
        this.mapper = mapper;
    }

    public Task<YeetsResult> ReadYeets(YeetsInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<YeetResult> ReadYeet(YeetInput input)
    {
        var yeet = await Table.FirstOrDefaultAsync(t => t.Id == input.YeetId);
        if (yeet is null) { return new YeetResult(null); }
        
        return new YeetResult(mapper.mapFrom(yeet));
    }

    public Task<FeedResult> ReadFeed(FeedInput input)
    {
        throw new NotImplementedException();
    }
}