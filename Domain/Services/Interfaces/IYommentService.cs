using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;

namespace Domain.Services.Interfaces;

public interface IYommentService
{
    Task<YommentsResult> Yomments(YommentsInput input);
    Task<YommentResult> Yomment(YommentInput inpot);
    Task<CreateYommentResult> CreateYomment(CreateYommentInput input);
    Task<DeleteYommentResult> DeleteYomment(DeleteYommentInput input);
    Task<AddLikeToYommentResult> AddLikeToYomment(AddLikeToYommentInput input);
}