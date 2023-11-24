using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("user_likes_yeets_jt")]
public class UserLikesYeet
{ 
    [Column("user_id"), Required]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Column("yeet_id"), Required]
    public int YeetId { get; set; }
    public Yeet Yeet { get; set; }
}