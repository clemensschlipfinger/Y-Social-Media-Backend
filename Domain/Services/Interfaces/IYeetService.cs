using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;

namespace Domain.Services.Interfaces;

public interface IYeetService
{
    Task<YeetsResult> Yeets(YeetsInput input);
    Task<YeetResult> Yeet(YeetInput input);
    Task<FeedResult> Feed(FeedInput input);
    Task<CreateYeetResult> CreateYeet(CreateYeetInput input);
    Task<DeleteYeetResult> DeleteYeet(DeleteYeetInput input);
}