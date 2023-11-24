using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("USER_LIKES_YEETS_JT")]
public class UserLikedYeet
{ 
    [Column("USER_ID"), Required]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Column("YEET_ID"), Required]
    public int YeetId { get; set; }
    public Yeet Yeet { get; set; }
}