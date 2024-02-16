namespace Backend.Graphql.Types.Inputs;

public record RegistrationInput
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
}