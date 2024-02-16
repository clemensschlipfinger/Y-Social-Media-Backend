namespace Domain.Graphql.Types.Inputs;

public record LoginInput
{
    public string Username { get; set; }
    public string Password { get; set; }
}