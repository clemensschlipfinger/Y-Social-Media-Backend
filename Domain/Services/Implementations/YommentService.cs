using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Services.Interfaces;

namespace Domain.Services.Implementations;

public class YommentService : IYommentService
{
    public async Task<YommentsResult> Yomments(YommentsInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<YommentResult> Yomment(YommentInput inpot)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateYommentResult> CreateYomment(CreateYommentInput input)
    {
        throw new NotImplementedException();
    }

    public async Task<DeleteYommentResult> DeleteYomment(DeleteYommentInput input)
    {
        throw new NotImplementedException();
    }
}