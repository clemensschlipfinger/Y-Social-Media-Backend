using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record CreateTagResult(Tag Tag, List<Tag> Tags);