namespace Domain.Graphql.Types.Inputs;

public record AddFollowInput
{
    public int UserId { get; set; }
    public int FollowingId { get; set; }
}