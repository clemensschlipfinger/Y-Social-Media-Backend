namespace Domain.Graphql.Types.Exceptions;

public class AlreadyFollowingException : Exception
{
    public AlreadyFollowingException(string follower, string following) : base($"User {follower} already follows {following}.")
    {
    }
    
}