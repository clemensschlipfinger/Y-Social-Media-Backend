namespace Domain.Graphql.Types.Inputs;

public record UserByUserNameInput
{
    public string UserName { get; set; }
}