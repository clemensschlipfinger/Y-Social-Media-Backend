using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("tag")]
public class Tag
{
    [Column("tag_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 
    
    [Column("name"), DataType(DataType.Text), Required]
    public string Name { get; set; }
}