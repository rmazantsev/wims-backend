using Domain.App.Identity;
using Domain.Base;

namespace Public.DTO.v1;

/// <summary>
/// Public.DTO.v1.StoreInventory 
/// </summary>
public class StoreInventory
{
    /// <summary>
    /// Id of Store
    /// </summary>
    public Guid Id { get; set; }

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