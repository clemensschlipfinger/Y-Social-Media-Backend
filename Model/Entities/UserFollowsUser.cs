using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;

namespace Model.Entities;


[Table("user_follows_users_jt")]
public class UserFollowsUser
{
    [Column("follower_id"),Required]
    public int FollowerId { get; set; }
    public User Follower { get; set; }
    
    [Column("following_id"),Required]
    public int FollowingId { get; set; }
    public User Following { get; set; }
}