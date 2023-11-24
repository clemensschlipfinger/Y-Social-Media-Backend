using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;

namespace Model.Entities;


[Table("user_follows_users_jt")]
public class UserFollowsUser
{
    [Column("slave_id"),Required]
    public int SlaveId { get; set; }
    public User Slave { get; set; }
    
    [Column("master_id"),Required]
    public int MasterId { get; set; }
    public User Master { get; set; }
}