namespace Domain.Graphql.Types;

public class Yeet
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public List<Tag> Tags { get; set; }
    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Yomment> Yomments { get; set; }
}