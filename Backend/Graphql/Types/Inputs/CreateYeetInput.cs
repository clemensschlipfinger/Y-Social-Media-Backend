namespace Backend.Graphql.Types.Inputs;

public class CreateYeetInput
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public List<int> Tags { get; set; }
}