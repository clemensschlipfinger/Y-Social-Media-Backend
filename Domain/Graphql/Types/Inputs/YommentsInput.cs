namespace Domain.Graphql.Types.Inputs;

public record YommentsInput
{
    public int YeetId { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public SortYomments Sorting { get; set; }
    public SortDirection Direction { get; set; } = SortDirection.ASC;
}