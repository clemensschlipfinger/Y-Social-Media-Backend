namespace Backend.Graphql.Types.Exceptions;

public class UsernameAlreadyExistsException : Exception
{
    public UsernameAlreadyExistsException(string username) : base($"Username {username} already exists.")
    {
    }
}