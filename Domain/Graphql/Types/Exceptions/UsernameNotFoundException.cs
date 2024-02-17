namespace Domain.Graphql.Types.Exceptions;

public class UsernameNotFoundException : Exception
{
    public UsernameNotFoundException(string username) : base($"User {username} not found.")
    {
    }
    
}