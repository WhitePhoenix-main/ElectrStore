using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectrStore;
[Table("Order")]
public class OrderRecord
{
    [Key] [Column(TypeName = "nvarchar(100)")] public string? Id { get; set; }
    [Column(TypeName = "nvarchar(100)")]public string? UserId { get; set; }
    public int Status { get; set; }
    public int TotalAmount { get; set; }
}