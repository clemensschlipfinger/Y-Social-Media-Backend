using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record LoginResult(string Token, User User);