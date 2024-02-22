using System.Linq.Expressions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IYeetRepository  : IRepository<Yeet>
{
    Task<YeetsResult> ReadYeets(YeetsInput input);
    Task<YeetResult> ReadYeet(YeetInput input);
    Task<FeedResult> ReadFeed(FeedInput input);
}