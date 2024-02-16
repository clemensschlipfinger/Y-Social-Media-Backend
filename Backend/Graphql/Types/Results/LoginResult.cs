using Model.Entities;

namespace Backend.Graphql.Types.Results;

public record LoginResult(string Token, User User);