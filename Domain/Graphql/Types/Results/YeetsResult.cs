using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record YeetsResult(List<Yeet> Result, int Count);