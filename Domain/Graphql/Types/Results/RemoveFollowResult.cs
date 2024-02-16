namespace Domain.Graphql.Types.Results;

public record RemoveFollowResult(int NewFollowingCount, int NewFollowerCount);