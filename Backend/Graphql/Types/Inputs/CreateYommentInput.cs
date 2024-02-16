namespace Backend.Graphql.Types.Inputs;

public record CreateYommentInput
{
    public int YeetId { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
}