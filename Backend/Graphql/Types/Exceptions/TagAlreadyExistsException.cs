namespace Backend.Graphql.Types.Exceptions;

public class TagAlreadyExistsException : Exception
{
    public TagAlreadyExistsException(string tagname) : base($"Tag {tagname} already exists.")
    {
    }
}