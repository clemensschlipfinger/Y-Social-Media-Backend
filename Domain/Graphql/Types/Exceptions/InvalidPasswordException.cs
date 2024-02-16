namespace Domain.Graphql.Types.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base($"Wrong password.")
    {
    }
}