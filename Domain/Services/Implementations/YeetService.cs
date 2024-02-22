using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations;

public class YeetService : IYeetService
{
    private IYeetRepository _yeetRepository;

    public YeetService(IYeetRepository yeetRepository)
    {
        _yeetRepository = yeetRepository;
    }

    public Task<YeetsResult> Yeets(YeetsInput input)
    {
        return _yeetRepository.ReadYeets(input);
    }

    public Task<YeetResult> Yeet(YeetInput input)
    {
        return _yeetRepository.ReadYeet(input);
    }

    public Task<FeedResult> Feed(FeedInput input)
    {
        return _yeetRepository.ReadFeed(input);
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