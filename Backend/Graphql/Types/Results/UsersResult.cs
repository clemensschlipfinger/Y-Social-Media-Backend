using Model.Entities;

namespace Backend.Graphql.Types.Results;

public record UsersResult(List<User> Result, int Count);