using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record TagsResult(List<Tag> Result, int Count);