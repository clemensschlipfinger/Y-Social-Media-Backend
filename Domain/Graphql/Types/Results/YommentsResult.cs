using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record YommentsResult(List<Yomment> Result, int Count);