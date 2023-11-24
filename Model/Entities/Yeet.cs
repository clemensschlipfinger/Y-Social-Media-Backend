using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;

namespace Model.Entities;

[Table("YEETS")]
public class Yeet
{
    [Column("YEET_ID")]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("BODY"),DataType(DataType.Text),Required]
    public string Body { get; set; }
    
    [Column("UserId"), Required]
    public int UserId { get; set; }
    public User User { get; set; }
}