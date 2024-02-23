namespace Domain.Graphql.Types.Exceptions;

public class PasswordHasNoDigitsException : Exception
{
    public PasswordHasNoDigitsException() : base("Password has no digits.")
    {
    }
}