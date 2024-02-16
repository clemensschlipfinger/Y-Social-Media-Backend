namespace Domain.Graphql.Types.Inputs;

public record UsersInput
{
    public int Limit { get; set; }
    public int Offset { get; set; }
    public SortUsers Sorting { get; set; }
    public SortDirection Direction { get; set; } = SortDirection.ASC;
    public string Filter { get; set; }
}