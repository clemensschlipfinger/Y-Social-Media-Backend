namespace Domain.Graphql.Types;

public record User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
    public List<UserInfo> Follower { get; set; }
    public List<UserInfo> Following { get; set; }
}