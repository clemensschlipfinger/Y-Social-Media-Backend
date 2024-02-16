namespace Backend.Graphql.Types.Inputs;

public class AddFollowInput
{
    public int UserId { get; set; }
    public int FollowingId { get; set; }
}