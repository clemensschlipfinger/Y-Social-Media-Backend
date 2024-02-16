namespace Domain.Graphql.Types.Inputs;

public record TagsInput
{
    public int Limit { get; set; }
    public int Offset { get; set; }
    public SortTags Sorting { get; set; }
    public SortDirection Direction { get; set; } = SortDirection.ASC;
    public string Filter { get; set; }
}