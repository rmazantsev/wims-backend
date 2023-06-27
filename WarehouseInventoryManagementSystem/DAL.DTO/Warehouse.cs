using DAL.Base;
using Domain.Base;

namespace DAL.DTO;

/// <summary>
/// DAL.DTO.v1.Warehouse 
/// </summary>
public class Warehouse: DomainEntityId
{
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