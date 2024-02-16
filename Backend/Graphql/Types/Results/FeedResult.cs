using Model.Entities;

namespace Backend.Graphql.Types.Results;

public record FeedResult(List<Yeet> Result, int Count);