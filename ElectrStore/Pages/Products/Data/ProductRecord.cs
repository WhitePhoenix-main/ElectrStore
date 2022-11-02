using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectrStore;
[Table(("Product"))]
public class ProductRecord
{
    [Key] [Column(TypeName = "nvarchar(100)")] public string? Id { get; set; }
    [Column(TypeName = "nvarchar(150)")] public string? ProductName { get; set; }
    public int Price { get; set; }
}