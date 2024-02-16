using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;

namespace Model.Entities;

[Table("yeets")]
public class Yeet
{
    [Key,Column("yeet_id"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("title"), DataType(DataType.Text), Required]
    public string Title { get; set; }
    
    [Column("body"),DataType(DataType.Text),Required]
    public string Body { get; set; }
    
    [Column("tag_id"), Required]
    public int TagId { get; set; }
    public Tag Tag { get; set; }
    
    [Column("likes"), Required]
    public int Likes { get; set; }
    
    [Column("created_at"),Required]
    public DateTime CreatedAt { get; set; }
    
    [Column("user_id"), Required]
    public int UserId { get; set; }
    public User User { get; set; }
}