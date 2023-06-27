

namespace Public.DTO.v1;

/// <summary>
/// Public.DTO.v1.WarehouseInventory 
/// </summary>
public class WarehouseInventory
{
    /// <summary>
    /// Warehouse Inventory id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Current amount of this item in Warehouse Inventory
    /// </summary>
    public long Quantity { get; set; }

    /// <summary>
    /// Optional location where this inventory is stored within warehouse
    /// </summary>
    public Guid? LocationId { get; set; }
    /// <summary>
    /// Location object
    /// </summary>
    public Location? Location { get; set; }

    /// <summary>
    /// Last time Warehouse Inventory was updated
    /// </summary>
    public DateTime? LastUpdated { get; set; }

    /// <summary>
    /// Warehouse Id to which inventory belongs
    /// </summary>
    public Guid WarehouseId { get; set; }
    /// <summary>
    /// Inventory store object 
    /// </summary>
    public Warehouse? Warehouse { get; set; }

    /// <summary>
    /// Item id of this inventory record
    /// </summary>
    public Guid ItemId { get; set; }
    /// <summary>
    /// Item object of this inventory record
    /// </summary>
    public Item? Item { get; set; } 
}