namespace Domain.Graphql.Types.Exceptions;

public class PasswordHasNoUpperCaseLettersException : Exception
{
    public PasswordHasNoUpperCaseLettersException() : base("Password has no upper case letters.")
    {
    }
}