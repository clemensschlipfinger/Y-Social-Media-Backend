namespace Domain.Graphql.Types.Exceptions;

public class PasswordTooShortException : Exception
{
    public PasswordTooShortException() : base("Password is too simple.")
    {
    }
}