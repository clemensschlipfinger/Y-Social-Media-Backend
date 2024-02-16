namespace Backend.Graphql.Types.Inputs;

public record RemoveFollowInput
{
    public int UserId { get; set; }
    public int FollowingId { get; set; }
}