namespace Backend.Graphql.Types.Inputs;

public class RemoveFollowInput
{
    public int UserId { get; set; }
    public int FollowingId { get; set; }
}