using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Model.Entities;

namespace Domain.Repositories.Interfaces;

public interface IYommentRepository : IRepository<Yomment>
{
    Task<YommentsResult> ReadYomments(YommentsInput input);
    Task<YommentResult> ReadYomment(YommentInput input);
    Task<Graphql.Types.Yomment?> ReadGraphqlYomment(int id);
    Task<bool> Exists(int yommentId);
    
}