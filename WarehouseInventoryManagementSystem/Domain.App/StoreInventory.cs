using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class StoreInventory : DomainEntityId
{
    public long Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
    
    public Guid StoreId { get; set; }
    public Store? Store { get; set; } 

    public Guid ItemId { get; set; }
    public Item? Item { get; set; } 
}