namespace Domain.Graphql.Types.Exceptions;

public class PasswordHasNoLowerCaseLettersException : Exception
{
    public PasswordHasNoLowerCaseLettersException() : base("Password has no lower case letters.")
    {
    }
}