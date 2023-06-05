using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectrStore;
[Table(("Product"))]
public class ProductRecord
{
    [Key] [Column(TypeName = "nvarchar(100)")] public string? Id { get; set; }
    [Column(TypeName = "nvarchar(150)")]public string? CategoryId { get; set; }
    [Column(TypeName = "nvarchar(200)")]public string? ProductName { get; set; } = "";
    public int Price { get; set; }
    public string? PreviewName { get; set; }
            
    public int Quantity { get; set; }
            
    public bool IsHotDeal { get; set; }
            
    public int DiscountPercent { get; set; }
            
    public bool IsDiscount { get; set; }

    public string? ProductDescription { get; set; } = "";
    
    public DateTime CreationTime { get; set; }

    public string EditorName { get; set; } = " ";

    [NotMapped] public bool OnPreview { get; set; }
    
    [NotMapped] public string? PriceInput { get; set; }
            
    [NotMapped] public string? ProductTypeNew { get; set; }
            
    [NotMapped] public string? ProductSearch { get; set; }

}