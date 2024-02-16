namespace Domain.Graphql.Types.Exceptions;

public class TagNotFoundException : Exception
{
    public TagNotFoundException(int id) : base($"Tag with id {id} not found.")
    {
    }
}