using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class WarehouseInventory: DomainEntityId
{
    public long Quantity { get; set; }
    public DateTime LastUpdated { get; set; }

    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; } 
    
    public Guid? LocationId { get; set; }
    public Location? Location { get; set; } 

    public Guid ItemId { get; set; }
    public Item? Item { get; set; } 
}