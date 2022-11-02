using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectrStore;
[Table(("Product"))]
public class ProductRecord
{
    [Key] public Guid Id { get; set; }
    [Column(TypeName = "nvarchar(150)")] public string? ProductName { get; set; }
}