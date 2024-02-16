using Model.Entities;

namespace Backend.Graphql.Types.Results;

public record YeetsResult(List<Yeet> Result, int Count);