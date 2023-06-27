using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class InventoryTransaction: DomainEntityId
{
    public long Quantity { get; set; }
    public char TransactionType { get; set; }
    [MaxLength(256)]
    public string? Description { get; set; }
    public DateTime TimeStamp { get; set; }
    public bool? IsCustomerPickup { get; set; }
    
    public Guid? FromLocation { get; set; }
    public Guid? ToLocation { get; set; }
    public Guid? FromStore { get; set; }
    public Guid? ToStore { get; set; }
    public Guid? FromWarehouse { get; set; }
    public Guid? ToWarehouse { get; set; }
    
    public Guid? StoreId { get; set; }
    public Store? Store { get; set; }
    
    public Guid? WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    public Guid ItemId { get; set; }
    public Item? Item { get; set; } 
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}