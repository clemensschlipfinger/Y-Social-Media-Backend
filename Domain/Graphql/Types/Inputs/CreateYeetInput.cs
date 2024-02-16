namespace Domain.Graphql.Types.Inputs;

public record CreateYeetInput
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public List<int> Tags { get; set; }
}