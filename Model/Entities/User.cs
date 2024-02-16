using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("users")]
public class User
{
   [Column("user_id")]
   [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int Id { get; set; } 
   
   [Column("username"), Required]
   public string Username { get; set; } 
   
   [Column("first_name"),Required]
   public string FirstName { get; set; } 
   
   [Column("last_name"),Required]
   public string LastName { get; set; } 
   
   [Column("password_hash"),Required]
   public string PasswordHash { get; set; }
}