namespace Backend.Graphql.Types.Exceptions;

public class YeetNotFoundException : Exception
{
    public YeetNotFoundException(int id) : base($"Yeet with id {id} not found.")
    {
    }
}