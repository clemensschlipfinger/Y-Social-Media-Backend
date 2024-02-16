using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record RegistrationResult(string Token, User User);