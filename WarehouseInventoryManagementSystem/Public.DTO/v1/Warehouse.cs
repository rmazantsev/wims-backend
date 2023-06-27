namespace Public.DTO.v1;

/// <summary>
/// Public.DTO.v1.Warehouse 
/// </summary>
public class Warehouse
{
    /// <summary>
    /// Warehouse Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Warehouse Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Warehouse Address
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// Inventories that belongs to this Warehouse
    /// </summary>
    public ICollection<WarehouseInventory>? WarehouseInventories { get; set; }

    /// <summary>
    /// Locations that belongs to this Warehouse
    /// </summary>
    public ICollection<Location>? Locations { get; set; }
}