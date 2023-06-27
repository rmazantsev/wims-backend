using BLL.Base;
using Domain.Base;

namespace BLL.DTO;

/// <summary>
/// BLL.DTO.StoreInventory 
/// </summary>
public class StoreInventory: DomainEntityId
{
    /// <summary>
    /// Quantity of specific item in store
    /// </summary>
    public long Quantity { get; set; }

    /// <summary>
    /// Last time inventory updated
    /// </summary>
    public DateTime? LastUpdated { get; set; }

    /// <summary>
    /// Store id to which inventory belongs
    /// </summary>
    public Guid StoreId { get; set; }
    /// <summary>
    /// Inventory store object 
    /// </summary>
    public Store? Store { get; set; }

    /// <summary>
    /// Item id
    /// </summary>
    public Guid ItemId { get; set; }
    
    /// <summary>
    /// Item object of this inventory record
    /// </summary>
    public Item? Item { get; set; } 
}