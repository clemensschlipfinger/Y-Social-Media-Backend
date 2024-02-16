using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("yeet_has_tags")]
public class YeetHasTags
{
    [Column("yeet_id"), Required]
    public int YeetId { get; set; }
    public Yeet Yeet { get; set; }
    
    [Column("tag_id"), Required]
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}