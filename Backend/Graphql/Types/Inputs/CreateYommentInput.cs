namespace Backend.Graphql.Types.Inputs;

public class CreateYommentInput
{
    public int YeetId { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
}