namespace Domain.Graphql.Types.Inputs;

public record YeetsInput
{
    public int Limit { get; set; }
    public int Offset { get; set; }
    public SortYeets Sorting { get; set; }
    public SortDirection Direction { get; set; } = SortDirection.ASC;
    public string? Filter { get; set; }
    public List<int>? Tags { get; set; }
}