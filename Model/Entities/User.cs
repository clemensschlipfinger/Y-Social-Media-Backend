using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("USERS")]
public class User
{
   [Column("USER_ID")]
   [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int Id { get; set; } 
   
   [Column("USERNAME"), Required]
   public string Username { get; set; } 
   
   [Column("FIRST_NAME"),Required]
   public string FirstName { get; set; } 
   
   [Column("LAST_NAME"),Required]
   public string LastName { get; set; } 
   
   [Column("PASSWORD_HASH"),Required]
   public string PasswordHash { get; set; }
    
}