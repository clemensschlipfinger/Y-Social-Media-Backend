using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations;

public class YeetService : IYeetService
{
    public async Task<YeetsResult> Yeets(YeetsInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<YeetResult> Yeet(YeetInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<FeedResult> Feed(FeedInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateYeetResult> CreateYeet(CreateYeetInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<DeleteYeetResult> DeleteYeet(DeleteYeetInput input)
    {
        throw new NotImplementedException();
    }
}