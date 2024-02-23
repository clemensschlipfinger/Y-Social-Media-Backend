namespace Domain.Graphql.Types;

public record Yomment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }
    public int YeetId { get; set; }
    public User User { get; set; }
}