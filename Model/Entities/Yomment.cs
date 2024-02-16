using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("yomments")]
public class Yomment
{
    [Key,Column("yomment_id"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("yeet_id"), Required]
    public int YeetId { get; set; }
    public Yeet Yeet { get; set; }
    
    [Column("body"),DataType(DataType.Text),Required]
    public string Body { get; set; }
    
    [Column("likes"), Required]
    public int Likes { get; set; }
    
    [Column("created_at"),Required]
    public DateTime CreatedAt { get; set; }
    
    [Column("user_id"), Required]
    public int UserId { get; set; }
    public User User { get; set; }
}