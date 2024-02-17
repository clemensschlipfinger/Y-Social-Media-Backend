namespace Domain.Graphql.Types.Exceptions;

public class NeverFollowedException : Exception
{
    public NeverFollowedException(string follower, string following) : base($"User {follower} has never followed {following}.")
    {
    }
}