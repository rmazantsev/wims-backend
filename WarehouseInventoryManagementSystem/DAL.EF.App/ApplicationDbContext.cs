using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DAL.EF.App;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Item> Items { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Unit> Units { get; set; } = default!;
    public DbSet<ItemComponent> ItemComponents { get; set; } = default!;
    public DbSet<Warehouse> Warehouses { get; set; } = default!;
    public DbSet<WarehouseInventory> WarehouseInventories { get; set; } = default!;
    public DbSet<Store> Stores { get; set; } = default!;
    public DbSet<StoreInventory> StoreInventories { get; set; } = default!;
    public DbSet<Location> Locations { get; set; } = default!;
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; } = default!;
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        foreach (var relationship in modelBuilder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        } 
        
        modelBuilder.Entity<Item>()
            .HasOne(p => p.Category)
            .WithMany(b => b.Items)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Warehouse>()
            .HasMany(i => i.InventoryTransactions)
            .WithOne(i=> i.Warehouse)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Store>()
            .HasMany(i => i.InventoryTransactions)
            .WithOne(i=> i.Store)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Item>()
            .HasMany<ItemComponent>(p => p.ItemComponents)
            .WithOne(b => b.Item)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Item>()
            .HasMany<ItemComponent>(p => p.OtherItemsComponents)
            .WithOne(b => b.ComponentItem)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<WarehouseInventory>()
            .Property(i => i.LastUpdated)
            .HasDefaultValueSql("NOW()");
        
        modelBuilder.Entity<StoreInventory>()
            .Property(i => i.LastUpdated)
            .HasDefaultValueSql("NOW()");
        
        modelBuilder.Entity<InventoryTransaction>()
            .Property(i => i.TimeStamp)
            .HasDefaultValueSql("NOW()");
    }
}