
using Public.DTO.v1.Identity;

namespace Public.DTO.v1;

/// <summary>
/// Public.DTO.v1.InventoryTransaction 
/// </summary>
public class InventoryTransaction
{
    /// <summary>
    /// InventoryTransaction id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Quantity of items affected in the transaction
    /// </summary>
    public long Quantity { get; set; }

    /// <summary>
    /// Enum. 'A' for addition of item, 'D' for deleting (withdrawing) of item, 'M' for movement of item
    /// </summary>
    public char TransactionType { get; set; }

    /// <summary>
    /// Optional description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Transaction date
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    /// Is it customer pickup
    /// </summary>
    public bool? IsCustomerPickup { get; set; }

    /// <summary>
    /// Id of location item moved from
    /// </summary>
    public Guid? FromLocation { get; set; }

    /// <summary>
    /// Id of location item moved to
    /// </summary>
    public Guid? ToLocation { get; set; }

    /// <summary>
    /// Id of store item moved from
    /// </summary>
    public Guid? FromStore { get; set; }

    /// <summary>
    /// Id of store item moved to
    /// </summary>
    public Guid? ToStore { get; set; }
    /// <summary>
    /// Id of warehouse item moved from
    /// </summary>
    public Guid? FromWarehouse { get; set; }
    /// <summary>
    /// Id of warehouse item moved to
    /// </summary>
    public Guid? ToWarehouse { get; set; }
    /// <summary>
    /// Transaction store id
    /// </summary>
    public Guid? StoreId { get; set; }
    /// <summary>
    /// Transaction store
    /// </summary>
    public Store? Store { get; set; }
    /// <summary>
    /// Transaction warehouse id
    /// </summary>
    public Guid? WarehouseId { get; set; }
    /// <summary>
    ///  Transaction warehouse
    /// </summary>
    public Warehouse? Warehouse { get; set; }

    /// <summary>
    /// Item id
    /// </summary>
    public Guid ItemId { get; set; }
    /// <summary>
    /// Transaction item
    /// </summary>
    public Item? Item { get; set; }
    /// <summary>
    /// Transaction user id
    /// </summary>
    public Guid AppUserId { get; set; }
    /// <summary>
    /// Transaction user
    /// </summary>
    public AppUser? AppUser { get; set; }
}
