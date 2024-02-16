using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;

namespace Domain.Services.Interfaces;

public interface IYommentService
{
    YommentsResult Yomments(YommentsInput input);
    YommentResult Yomment(YommentInput inpot);
    CreateYommentResult CreateYomment(CreateYommentInput input);
    DeleteYommentResult DeleteYomment(DeleteYommentInput input);
}