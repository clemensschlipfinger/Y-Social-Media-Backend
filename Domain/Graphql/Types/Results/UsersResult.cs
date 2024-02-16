using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record UsersResult(List<User> Result, int Count);