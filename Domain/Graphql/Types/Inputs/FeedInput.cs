namespace Domain.Graphql.Types.Inputs;

public record FeedInput
{
    public int UserId { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
}