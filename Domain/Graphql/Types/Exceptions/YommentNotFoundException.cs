namespace Domain.Graphql.Types.Exceptions;

public class YommentNotFoundException : Exception
{
    public YommentNotFoundException(int id) : base($"Yomment with id {id} not found.")
    {
    }
}