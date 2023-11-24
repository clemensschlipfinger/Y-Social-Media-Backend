using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;

namespace Model.Entities;


[Table("USER_FOLLOWS_USERS_JT")]
public class UserFollowsUser
{
    [Column("Slave"),Required]
    public int SlaveId { get; set; }
    public User Slave { get; set; }
    
    [Column("MASTER"),Required]
    public int MasterId { get; set; }
    public User Master { get; set; }
}