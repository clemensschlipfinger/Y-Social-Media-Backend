using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record FeedResult(List<Yeet> Result, int Count);