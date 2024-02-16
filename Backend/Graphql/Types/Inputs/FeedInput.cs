namespace Backend.Graphql.Types.Inputs;

public class FeedInput
{
    public int UserId { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
}