namespace Backend.Graphql.Types.Results;

public record TagsResult(List<Tag> Result, int Count);