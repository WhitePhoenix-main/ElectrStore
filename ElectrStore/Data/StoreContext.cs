using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore;

public class StoreContext : IdentityDbContext
{
    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
        
    }

    public DbSet<ProductRecord> ProductRecords { get; set; } = null!;
    public DbSet<OrderRecord> OrderRecords { get; set; } = null!;
    public DbSet<OrderItemsRecord> OrderItemsRecords { get; set; } = null!;
}