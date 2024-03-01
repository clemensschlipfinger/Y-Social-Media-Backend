using Model.Entities;

namespace Domain.Graphql.Types.Results;

public record UserByUserNameResult(User? Result);