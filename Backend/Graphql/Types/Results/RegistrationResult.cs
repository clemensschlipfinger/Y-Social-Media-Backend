using Model.Entities;

namespace Backend.Graphql.Types.Results;

public record RegistrationResult(string Token, User User);