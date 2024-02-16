namespace Backend.Graphql.Types.Results;

public record CreateTagResult(Tag Tag, List<Tag> Tags);