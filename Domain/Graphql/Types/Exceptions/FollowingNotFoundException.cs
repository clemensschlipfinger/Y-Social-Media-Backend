namespace Domain.Graphql.Types.Exceptions;

public class FollowingNotFoundException : Exception
{
    public FollowingNotFoundException(int id) : base($"Following user with id {id} not found.")
    {
    }
}