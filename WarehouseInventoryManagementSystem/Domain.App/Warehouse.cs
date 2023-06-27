using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Warehouse: DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = null!;
    [MaxLength(128)]
    public string Address { get; set; } = null!;

    public ICollection<WarehouseInventory>? WarehouseInventories { get; set; }
    public ICollection<InventoryTransaction>? InventoryTransactions { get; set; }
    public ICollection<Location>? Locations { get; set; }
}
