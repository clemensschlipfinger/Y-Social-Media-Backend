using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;

namespace Domain.Services.Interfaces;

public interface IYeetService
{
    YeetsResult Yeets(YeetsInput input);
    YeetResult Yeet(YeetInput input);
    FeedResult Feed(FeedInput input);
    CreateYeetResult CreateYeet(CreateYeetInput input);
    DeleteYeetResult DeleteYeet(DeleteYeetInput input);
}